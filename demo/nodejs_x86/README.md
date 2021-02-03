我是将nodejs版本专门降到v10.21.0才跑得起来
# 以管理员运行
```cmd
npm install -g --production windows-build-tools
npm install -g node-gyp
```
# 安装依赖
```cmd
npm install ffi
npm install ref 
npm install iconv-lite 
```
# 拷贝dll
将HandWriteRecognizerCSharp.dll和Microsoft.Ink.dll拷贝到nodejs的安装目录（即和node.exe同目录）。
# 执行 
```cmd
node index.js
```