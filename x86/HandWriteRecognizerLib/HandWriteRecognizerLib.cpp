#include <iostream>
#include <fstream>
using namespace std;
using namespace System::Runtime::InteropServices;
using namespace System;
using namespace HandWriteRecognizerCSharp;
extern "C"  __declspec(dllimport)   char* recognizer(char* strokes, int count);
extern "C"  __declspec(dllimport)   void writeLog(char* str);
char* recognizer(char* strokes, int count) {
	char* result = NULL;
	try {
		InkAnalyzer^ inkAnalyzer = gcnew InkAnalyzer();
		String^ strokesStr = Marshal::PtrToStringAnsi((IntPtr)strokes);
		System::String^ resultStr = inkAnalyzer->Recognizer(strokesStr, count);
		result = (char*)(void*)Marshal::StringToHGlobalAnsi(resultStr); 
	}
	catch (char* str)        // 捕获所有异常
	{
		writeLog(str);
	}
	return result;
}
void writeLog(char* str) {
	ofstream outfile;
	outfile.open("error.log", ios::app);
	string s = string(str);
	outfile << s << endl;
	outfile.close();
}