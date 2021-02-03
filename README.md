# HandWriteRecognizer
封装Microsoft.Ink为C++动态库，可供其他语言调用手写识别

# 在java中使用

根据windows系统下载release中的win32.zip或win64.zip。其中HandWriteRecognizerLib.dll为C++动态库，Microsoft.Ink.dll为微软手写识别库，HandWriteRecognizerCSharp.dll是C#封装的中间层。
将C#的两个dll拷贝到java的bin目录，如果java是32位拷贝win32的，如果java是64位的拷贝win64的。

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
    //虽然loadLibrary已过时，不过试了load在springboot网站无法正常加载，原因未知。
    HandWriteRecognizerLibrary INSTANCE = Native.loadLibrary("HandWriteRecognizerLib", HandWriteRecognizerLibrary.class);
    Pointer recognizer(Pointer strokes, int count); 

} 
```
### 调用jna接口

```
Pointer strokesStrPointer = new Memory(strokesStr.length());
byte[] bytes = strokesStr.getBytes();
strokesStrPointer.write(0, bytes, 0, bytes.length);
Pointer result = new Memory(count * 2);
result = HandWriteRecognizerLibrary.INSTANCE.recognizer(strokesStrPointer, count);
String temp = result.getString(0);
if (StringUtils.isEmpty(temp)) {
    return new String[0];
} else {
    return temp.split(" ");
} 
```
# 打包说明
请用vs2019（C++编译我不在行，使用其他IDE我不清楚怎么整）打开项目文件HandWriteRecognizer.sln。先右键HandWriteRecognizerCSharp项目生成后，再右键HandWriteRecognizerLib生成项目。最终生成目录在根目录的Debug或Release文件夹。将文件夹中的三个dll拷贝出来即可。