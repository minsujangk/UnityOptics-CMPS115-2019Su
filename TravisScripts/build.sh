#! /bin/sh

# Builds Unity3D project
project="unityoptics-build"

## Run the editor unit tests
echo "Running editor unit tests for ${UNITYCI_NAME}"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
	-batchmode \
	-nographics \
	-silent-crashes \
	-logFile $(pwd)/unity.log \
	-projectPath "$(pwd)/${UNITYCI_NAME}" \
	-runEditorTests \
	-editorTestsResultFile $(pwd)/test.xml \
	-quit

rc0=$?
echo "Unit test logs"
cat $(pwd)/test.xml
# exit if tests failed
if [ $rc0 -ne 0 ]; then { echo "Failed unit tests"; exit $rc0; } fi

echo "Attempting build of ${UNITYCI_NAME} for OSX"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
	-batchmode \
	-nographics \
	-silent-crashes \
	-logFile $(pwd)/unity.log \
	-projectPath "$(pwd)/${UNITYCI_NAME}" \
	-buildOSXUniversalPlayer "$(pwd)/Build/osx/${UNITYCI_NAME}.app" \
	-quit

# Print build logs 
echo 'Logs from build'
cat $(pwd)/unity.log
