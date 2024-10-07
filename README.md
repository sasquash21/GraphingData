# Unemployment Data Graphing Application

Authored by **Sashank Rao** as part of a C# graphing experience. (Still a work in progress)

## Project Overview

This project is a graphical user interface (GUI) application that visualizes unemployment data by state from 1976 to 2024. The application allows users to select specific states, customize axis scaling, and generate a graph using the OxyPlot library. The graph can be saved as an image file, and the application's settings (such as selected states and axis scales) are remembered between restarts.

## Design Specifications

This project required a combination of new and past experiences, including:

- **Data Formatting and Reading**: The data was provided as an Excel file, which was converted into CSV format for ease of use. CSVHelper was used to parse and load the data into the application.
  
- **GUI Elements**: 
  - A `CheckedListBox` was used to list all 50 states, along with LA County and New York City.
  - Numeric up and down boxes were provided for x axis ranges to allow users to view data for specific years (new). 
  - Text input boxes were provided for vertical and horizontal scalers to allow users to customize the graph.

## Key Features

### Graphing
For graphing, I chose **OxyPlot**, which provided the versatility needed for this project. Users can:
- Select which states they want to view using the `CheckedListBox`.
- Adjust the horizontal (year) and vertical (unemployment %) scales using the text boxes. If no valid input is provided, the graph auto-scales to fit the data.
- Save the graph as a PNG file, with a legend for clarity.

### Undo Functionality
- An `Undo` button allows users to revert changes made to the graph. The application saves the graph state using the `GraphState` struct and stores them in a stack.
- Users can undo up to the last 7 actions, and if the limit is exceeded, the program notifies the user.

### Saving Settings for Restarts
To ensure that settings such as selected states and scales are remembered between application restarts, I implemented functionality using the `Settings.settings` file. This was a new experience for me, and involved creating custom methods for loading and saving settings (`LoadSettings` and `SaveSettings`), which are called during form load and close events.

## Testing

I focused on ensuring that the GUI provided a smooth user experience without breaking. Key test areas included:
- **Axes Scaling**: The vertical scale is constrained to values greater than 0 and less than 6 for a visually balanced graph. The horizontal scale is set between 0 and 20. If the user inputs invalid scale values, the graph does not adjust, and no warning is issued—this would be a discussion point for an enterprise project.
- **Auto-scaling**: If no scaling values are provided, the graph auto-scales to fit all data.


