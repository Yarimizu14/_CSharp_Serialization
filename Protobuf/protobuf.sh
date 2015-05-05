#!/bin/sh
echo "Info: Start converting .proto to .cs"

while getopts i:o: OPT
do
    case $OPT in
        "i" ) INPUT_PATH=$OPTARG;;
        "o" ) OUTPUT_PATH=$OPTARG;;
    esac
done

echo "Info: input from $INPUT_PATH"
echo "Info: output to $OUTPUT_PATH"

# check input path is file
if [ -f "$INPUT_PATH" ]; then
    :
else
    echo "Error: Input proto file is not exist"
    exit 1
fi

# check output path is directory
if [ -d "$OUTPUT_PATH" ]; then
    :
else
    echo "Error: Output path is not exist or not directory"
    exit 1
fi

# parse input file
filename=`basename $INPUT_PATH`
filename_without_ext=${filename%.*}
ext=${filename##*.}

# check file is proto file
if [ "$ext" != "proto" ]; then
    echo "Error: File is not proto file"
    exit 1
fi

# check protoc installed
if ! type protoc > /dev/null; then
    echo "Error: Install protoc command"
fi

# check mono installed
if ! type mono > /dev/null; then
    echo "Error: Install protoc command"
fi

OUTPUT_PB_FILE="${filename_without_ext}.pb"
OUTPUT_CS_FILE="${filename_without_ext}.cs"

protoc $INPUT_PATH --descriptor_set_out=$OUTPUT_PB_FILE
mono ./NET30/protogen.exe -i:$OUTPUT_PB_FILE -o:$OUTPUT_CS_FILE

# remove pb file
rm $OUTPUT_PB_FILE

