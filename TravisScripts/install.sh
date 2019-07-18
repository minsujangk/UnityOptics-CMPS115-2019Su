#! /bin/sh

# Installs Unity3D in Travis container

echo 'Downloading from https://beta.unity3d.com/download/8ea4afdbfa47/MacEditorInstaller/Unity.pkg: '
curl -o Unity.pkg https://beta.unity3d.com/download/8ea4afdbfa47/MacEditorInstaller/Unity.pkg
if [ $? -ne 0 ]; then { echo "Download failed"; exit $?; } fi


echo 'Installing Unity.pkg'
sudo installer -dumplog -package Unity.pkg -target /

