import os
import yaml
from glob import glob
import file_helper


from yaml import load, dump
try:
    from yaml import CLoader as Loader, CDumper as Dumper
except ImportError:
    from yaml import Loader, Dumper

    
#Read configuration file
stream = open("config.ini", 'r')
config = yaml.load(stream, Loader=Loader)

_layout = config["layout"]
_source = config["source"]

#Get layout page
layout = file_helper.read_file(_layout);

#Get list of files to transform
files = [y for x in os.walk(_source) for y in glob(os.path.join(x[0], '*.md'))]

for file in files:
    file_helper.transform_file(file, layout)