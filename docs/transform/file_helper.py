import mistune
import os
import shutil
import re
import htmlmin
from bs4 import BeautifulSoup


def read_file(path):
    with open(path, "r") as content_file: return content_file.read()

def create_directory(path):
    os.mkdir(path)

def copy_directory(path, destination):
    directory = os.path.basename(os.path.normpath(path))
    
    from distutils.dir_util import copy_tree
    copy_tree(path, os.path.join(destination, directory))

def delete_directory(path):
    if os.path.isdir(path):
        shutil.rmtree(path)
    
def write_file(path, contents):
    from pathlib import Path
    file = Path(path)

    if not file.parent.exists():
        file.parent.mkdir(parents=True)

    with open(path, "w") as writer:
        writer.write(contents)

def transform_links(contents):
    return contents.replace(".md", ".html")

def get_title(contents):
    soup = BeautifulSoup(contents)
    headers = soup.find_all(['h1', 'h2', 'h3', 'h4', 'h5', 'h6'])

    try:
        return headers[0].contents[0] 
    except IndexError:
        return "MixERP Documentation"

def transform_file(file, layout):
    destination = file.replace(".md", ".html");
    
    markup = mistune.markdown(read_file(file));
    markup = transform_links(markup)
    
    title = get_title(markup);

    contents = layout.replace("{document}", markup).replace("{title}", title);
    contents = htmlmin.minify(contents, remove_empty_space=True)

    print("Writing " + destination)
    write_file(destination, contents)