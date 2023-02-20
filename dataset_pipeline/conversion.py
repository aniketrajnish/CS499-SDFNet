#!/usr/bin/env python3
# -*- coding: utf-8 -*-

import sys, getopt
import h5py
import numpy as np
import os
from PIL import Image
    
    
def preprocess(image_path, num_px):
    image = Image.open(image_path)
    resized_image = image.resize((num_px,num_px))
    image_arr = np.array(resized_image)
    return image_arr

def convert_dir(input_dir, output_file_name, dimention):
    arr = os.listdir(input_dir)
    result_arr = np.empty([len(arr), dimention, dimention, 4],dtype='int16');
    
    for i in range(0,len(arr)):
        f_path = input_dir + '/' +arr[i]
        im_array=preprocess(f_path, dimention)
        result_arr[i]= im_array
    
    #convert to h5 file
    h5f = h5py.File(output_file_name + ".h5", 'w')
    h5f.create_dataset('bottles', data=result_arr)


def get_params():
    ''' example command: python for_hdf5.py D:\my_work\GraphicsModelRendering\Iteration_1\BottleRenders\BottleRenders output'''
    

    input_dir = sys.argv[1]
    output_file = sys.argv[2]
    
    print ('Input directory is: ', input_dir)
    print ('Output file is: ', output_file)
   
    return input_dir, output_file
    
if __name__ == "__main__":
        

    input_dir, output_file = get_params()
   
   
    convert_dir(input_dir, output_file, 300)
    print ('Conversion successful. Output: ', output_file)



