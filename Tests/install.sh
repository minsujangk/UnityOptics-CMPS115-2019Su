#! /bin/sh

# Installs Unity3D in Travis container

echo 'Downloading from http://netstorage.unity3d.com/unity/3757309da7e7/MacEditorInstaller/Unity-5.2.2f1.pkg: '
curl -o Unity.pkg http://netstorage.unity3d.com/unity/3757309da7e7/MacEditorInstaller/Unity-5.2.2f1.pkg
if [ $? -ne 0 ]; then { echo "Download failed"; exit $?; } fi


echo 'Installing Unity.pkg'
sudo installer -dumplog -package Unity.pkg -target /


