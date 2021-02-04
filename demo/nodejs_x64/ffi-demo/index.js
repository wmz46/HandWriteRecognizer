var ffi = require('ffi');   
var ref = require('ref');   
var iconv = require("iconv-lite");
var stringPtr = ref.refType(ref.types.CString);
var handwrite = ffi.Library('HandWriteRecognizerLib', {
  'recognizer': [stringPtr , [ 'string','int' ] ]
});
var result = handwrite.recognizer('8,5,10,5,18,32,42,5,20,5,50,3',5) 
//解决中文编码问题
console.info(iconv.decode(result,'GBK'))