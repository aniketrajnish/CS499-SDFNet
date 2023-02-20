# csg-large

## Overview

This dataset is generated using [SDFNet](https://github.com/aniketrajnish/CS499-SDFNet), a proprietary rendering tool, to be used in the implementation of Constructive Solid Geometry (CSG) algorithms. The dataset includes a comprehensive set of [number] 3D shapes, each with its corresponding CSG representation/annotations/other relevant information.

## Pipeline Structure

1. Model Generation: Use SDFNet to create a diverse set of 3D models, representing a wide range of shapes and forms. This could include simple shapes like spheres and cubes, as well as more complex shapes.

2. CSG Annotation: Annotate each 3D model with its corresponding CSG representation. This could involve breaking down each model into its component parts and representing them using Boolean operations.

3. Rendering: Render each 3D model from a variety of viewpoints and lighting conditions, to create a diverse set of 2D images. This could include both RGB and depth images.

4. Preprocessing: Perform any necessary preprocessing on the dataset, such as resizing or cropping images, normalizing depth values, or removing background elements.

5. Dataset preparation: Prepare the dataset for use in machine learning models by splitting it into training, validation, and test sets. This could involve randomly selecting a subset of the data for each set or using a specific methodology like k-fold cross validation.

6. Saving and Exporting: Save the prepared dataset in a format that can be easily loaded and used in machine learning models. This could include exporting it as a CSV file, a set of image files, or a binary file that can be loaded into memory. We will, very likely, be using .h5 format for the images.

## Usage

Coming up real soon!
