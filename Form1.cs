using System;
using CsvHelper;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Globalization;
using OxyPlot.Axes;
using Microsoft.VisualBasic.ApplicationServices;
using System.Security.Policy;
using OxyPlot.Legends;
using System.Windows.Forms.PropertyGridInternal;
using System.Windows.Forms.Design;
using System.Runtime.CompilerServices;
using System.ComponentModel.Design;

namespace GraphingData
{
    //Struct for Capturing states and storing in Stack for Undo button
    public struct GraphState
    {
        public List<string> SelectedStates { get; set; }
        public string HorizontalScale { get; set; }
        public string VerticalScale { get; set; }
    }
    public partial class Form1 : Form
    {

        private OxyPlot.PlotModel plotModel;
        private DataTable unemploymentData;
         
        public Form1()
        {
            InitializeComponent();
            LoadData();
            InitializePlot();
        }

        //Using CsvHelper parse through data
        private void LoadData()
        {
            try
            {
                string csvFilePath = @"./Data/Unemployment Data By State, 1976-2024.csv";
                using (var reader = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<dynamic>().ToList();
                    unemploymentData = new DataTable();

                    foreach (var record in records)
                    {
                        var row = unemploymentData.NewRow();
                        foreach (var kvp in record)
                        {
                            if (!unemploymentData.Columns.Contains(kvp.Key))
                            {
                                unemploymentData.Columns.Add(kvp.Key);
                            }
                            row[kvp.Key] = kvp.Value;
                        }
                        unemploymentData.Rows.Add(row);
                    }

                    foreach(DataColumn col in unemploymentData.Columns)
                    {
                        if(col.ColumnName != "year" && col.ColumnName != "month")
                        {
                            checkedListBoxStates.Items.Add(col.ColumnName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading CSV file: {ex.Message}");
            }
        }

        //Create the plot here and set up the axes
        private void InitializePlot()
        {
            plotModel = new PlotModel { Title = "Unemployment Data by State" };
            plotView.Model = plotModel;

            Legend legend = new Legend();

            // Add DateTime X-Axis
            var dateAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "yyyy",
                Title = "Date",
                IntervalType = DateTimeIntervalType.Years,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
            };

            plotModel.Axes.Add(dateAxis);

            // Add Unemployment % Y-Axis
            var valueAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Unemployment Rate (%)",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
            };

            plotModel.Axes.Add(valueAxis);

        }

        //Redraw the plot button after putting in the necessary graph settings for axes and selected states
        private void RedrawPlot(List<string> selectedStates)
        {
            plotModel.Series.Clear(); // Clear existing series
            
            //Adding the Legend each time Redraw is called.  Used for PNG images.
            plotModel.Legends.Add(new Legend()
            {
                LegendTitle = "States",
                LegendPosition = LegendPosition.RightTop, // Set the position of the legend
                LegendPlacement = LegendPlacement.Outside,
                LegendOrientation = LegendOrientation.Vertical,
            });
            
            foreach (var state in selectedStates)
            {
                var series = new LineSeries {Title = state};

                foreach (DataRow row in unemploymentData.Rows)
                {
                    var year = int.Parse(row["year"].ToString());
                    var month = int.Parse(row["month"].ToString());
                    var date = new DateTime(year, month, 1); // Construct the date from year and month

                    if (double.TryParse(row[state].ToString(), out double unemploymentRate))
                    {
                        series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), unemploymentRate));
                    }
                }

                plotModel.Series.Add(series); // Add the series for each state
            }

            plotModel.InvalidatePlot(true); // Refresh the plot to show new series
        }

        //Button for Redraw function
        private void buttonRedraw_Click(object sender, EventArgs e)
        {
            var selectedStates = checkedListBoxStates.CheckedItems.Cast<string>().ToList();

            if(selectedStates.Count > 0)
            {
                RedrawPlot(selectedStates);
            }
            AdjustScales();
            CurrentGraphState();
        }

        //Button for saving the image
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Image|*.png",
                Title = "Save Graph as Image"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var pngExporter = new PngExporter { Width = 800, Height = 600 };
                pngExporter.ExportToFile(plotModel, saveFileDialog.FileName);
            }
        }

        //This function sets the axes scales
        private void AdjustScales() 
        {

            //If no input in horizontal or vertical scaler text boxes then we want to autoscale

            bool manualHorizontalScale = false;
            bool manualVerticalScale = false;


            // Code to change horizontal scale and check if it is not within 1-20
            if (double.TryParse(textBoxHorizontalScale.Text, out double horizontalScale) && horizontalScale > 0 && horizontalScale <= 20)
            {
                plotModel.Axes[0].MajorStep = horizontalScale;
                manualHorizontalScale = true;
            }

            // Code to change vertical scale and check if it is not within 1-5
            if (double.TryParse(textBoxVerticalScale.Text, out double verticalScale) && verticalScale > 0 && verticalScale < 6)
            {
                plotModel.Axes[1].MajorStep = verticalScale;
                manualVerticalScale = true;
            }

            if (!manualHorizontalScale)
            {
                plotModel.Axes[0].Reset();
            }
            if (!manualVerticalScale)
            {
                plotModel.Axes[1].Reset();
            }

            plotModel.InvalidatePlot(true);
        }

        //Saves the current state of the UI through windows Forms Settings in Properties
        private void SaveSettings()
        {
            //Save selectedStates
            var selectedStates = string.Join(",", checkedListBoxStates.CheckedItems.Cast<string>());
            Properties.Settings.Default.SelectedStates = selectedStates;

            //Save Horizontal Scale
            if (int.TryParse(textBoxHorizontalScale.Text, out int horizontalScale))
            {
                Properties.Settings.Default.HorizontalScale = horizontalScale;
            }

            //Save Vertical Scale
            if (int.TryParse(textBoxVerticalScale.Text, out int verticalScale))
            {
                Properties.Settings.Default.VerticalScale = verticalScale;
            }

            Properties.Settings.Default.Save();
        }

        //Loads the saved settings in the windows forms property settings
        private void LoadSettings()
        {
            // Load selected states
            var savedStates = Properties.Settings.Default.SelectedStates;
            if (!string.IsNullOrEmpty(savedStates))
            {
                var statesArray = savedStates.Split(',');
                foreach (string state in statesArray)
                {
                    int index = checkedListBoxStates.Items.IndexOf(state);
                    if (index >= 0)
                    {
                        checkedListBoxStates.SetItemChecked(index, true);
                    }
                }
            }
            // Load horizontal and vertical scales
            textBoxHorizontalScale.Text = Properties.Settings.Default.HorizontalScale.ToString();
            textBoxVerticalScale.Text = Properties.Settings.Default.VerticalScale.ToString();
        }

        //Performs the the load settings function when the application is opened by user
        private void programOpenLoad(object sender, EventArgs e)
        {
            LoadSettings();
        }

        //Performs save settings function when the application is closed by user
        private void programClosingSave(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        //Undo Button Create Stack for the previous 7 moves

        private Stack<GraphState> undoStack = new Stack<GraphState>();
        private const int UndosAllowed = 7;
        //Saves current state of the graph in structure and stores in the stack
        private void CurrentGraphState()
        {
            //7 undos
            if (undoStack.Count >= UndosAllowed)
            {
                undoStack = new Stack<GraphState>(undoStack.Skip(1));
            }
            var currentState = new GraphState
            {
                SelectedStates = checkedListBoxStates.CheckedItems.Cast<string>().ToList(),
                HorizontalScale = textBoxHorizontalScale.Text,
                VerticalScale = textBoxVerticalScale.Text,
            };

            undoStack.Push(currentState);
        }

        //Puts back previous settings of the graph when undo button is hit
        private void RestoreState(GraphState state)
        {
            for (int i = 0; i < checkedListBoxStates.Items.Count; i++)
            {
                checkedListBoxStates.SetItemChecked(i, state.SelectedStates.Contains(checkedListBoxStates.Items[i].ToString()));
            }

            textBoxHorizontalScale.Text = state.HorizontalScale;
            textBoxVerticalScale.Text = state.VerticalScale;

            RedrawPlot(state.SelectedStates);
        }

        //Calls on RestoreState to pop stack and restore previous states
        private void UndoPrev()
        {
            if (undoStack.Count > 0)
            {
                //Pop last action from stack
                var prevState = undoStack.Pop();

                //Restore State
                RestoreState(prevState);
            }
            else
            {
                MessageBox.Show("Unable to Undo.");
            }
        }

        //Undo button event
        private void buttonUndo_Click(object sender, EventArgs e)
        {
            UndoPrev();
        }
    }
}

