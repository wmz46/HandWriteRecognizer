# HandWriteRecognizer
封装Microsoft.Ink为C++动态库，可供其他语言调用手写识别

# 在java中使用方法

下载release中的win32，HandWriteRecognizerLib.dll为C++动态库，Microsoft.Ink.dll为微软手写识别库，HandWriteRecognizerCSharp.dll是C#封装的中间层。
将C#的两个dll拷贝到java的bin目录，这里是32位，所以java也必须是32位。

jna引用HandWriteRecognizerLib.dll

## 识别接口：
 char* recognizer(char* strokes, int count)
 
 strokes入参为笔画字符串，多个笔画用,eb,分割，笔画中的每个点按x1,y1,x2,y2,x3,y3顺序拼接。如x1,y1,x2,y2,x3,y3,eb,x1,y1,x2,y2,...
 
 count入参为返回的最大识别字符数，返回接口可能小于等于该数。
 
 返回为识别结果字符串，字符以英文空格间隔。

## 调用示例
### 定义jna接口

```
public interface HandWriteRecognizerLibrary  extends Library {
    HandWriteRecognizerLibrary INSTANCE = Native.load("HandWriteRecognizerLib", HandWriteRecognizerLibrary.class);
    Pointer recognizer(Pointer strokes, int count); 

} 
```
### 调用jna接口

```
Pointer result = new Memory(count * 2);
result = HandWriteRecognizerLibrary.INSTANCE.recognizer(strokesStrPointer, count);
String temp = result.getString(0);
if (StringUtils.isEmpty(temp)) {
    return new String[0];
} else {
    return temp.split(" ");
} 
```
