# 3D Scatter Plot Data Visualizer in Unity VR

This Unity VR application provides a powerful and intuitive way to visualize large datasets in 3D space. It is designed to handle both discrete and continuous values, offering unique functionality to improve clarity in dense and overlapping data regions. For continuous data, users can apply rounding, ceiling, or flooring operations to group similar values, enabling a more focused and customizable analysis.

## 🌟 Features
* **Immersive VR Visualization:** Explore datasets in a fully interactive 3D environment.
* **Discrete and Continuous Value Support:** Works seamlessly with both discrete categories and continuous numerical data.
* **Dynamic Point Dispersion:** Generates equidistant points around overlapping data to improve trend and concentration visualization.
* **Value Grouping Options:**
    * **Rounding:** Groups continuous values by rounding to the nearest significant figure.
    * **Ceiling:** Groups values to the nearest upper bound.
    * **Flooring:** Groups values to the nearest lower bound.
* **Customizable Display:** Modify visual elements such as plot scale, point size, and color, to suit your data and analysis needs.

## 🎥 Demonstrations

**Visualizing Continuous Values**

When working with continuous values, trends can be difficult to discern due to the sheer density and overlapping of data points. This application offers grouping options such as rounding, ceiling, or flooring to cluster values, making trends more visible and analysis more intuitive. Check out the video below to see how grouping enhances clarity:

<div align="center">
   <h3>Before Grouping</h3>
  <img src="./Images/Cont_NoGrouping.gif" alt="Before Grouping" />
</div>

<div align="center">
   <h3>Grouping for Clarity</h3>
  <img src="./Images/Cont_Grouped.gif" alt="Grouping for Clarity" />
</div>

**Handling Discrete Overlaps**

For discrete values, traditional scatter plots often fail to display overlapping points clearly, merging them into a single datapoint. Our program disperses these values equidistantly around their true position, allowing you to visualize the density of overlaps and identify patterns more effectively. Watch the video below for a demonstration:

## 💡 Use Cases
* 🔍 Exploratory data analysis
* 📊 Trend identification in large or complex datasets
* 🎓 Academic research requiring immersive data exploration
* 🌐 Visualization of high-dimensional or dense data

## Contributing
Contributions are welcome! If you have ideas for improvements or encounter a bug, feel free to open an issue or submit a pull request.

