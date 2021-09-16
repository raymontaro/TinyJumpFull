mergeInto(LibraryManager.library, {

  // this function prints a “Hello” message to the console
  PrintHello: function () {
    console.log("[JavaScript] PrintHello: " + "Hello");
  },

  // this function prints a string, provided as an argument, to the console
  PrintString: function (pStr) {
    var str = UTF8ToString(pStr); // create a JavaScript string from a null-terminated UTF-8 string allocated on the heap, pointed by pStr
    console.log("[JavaScript] PrintString: " + str);
  },
  
  // this function adds two numbers and returns the result
  AddNumbers: function (x, y) {
    console.log("[JavaScript] AddNumbers: " + x + " + " + y);
    return x + y;
  },

  // this function returns a string to C#
  ReceiveString: function () {
    var str = "A string passed from JavaScript to C#";
    var bufferSize = lengthBytesUTF8(str) + 1; // calculate the size of null-terminated UTF-8 string
    var buffer = _malloc(bufferSize); // allocate string buffer on the heap
    stringToUTF8(str, buffer, bufferSize); // fill the buffer with the string UTF-8 value
    return buffer; // return the pointer of the allocated string to C#
  },

  // this function returns a string to C#
  ReceiveUser: function () {
    var str = user;
    var bufferSize = lengthBytesUTF8(str) + 1; // calculate the size of null-terminated UTF-8 string
    var buffer = _malloc(bufferSize); // allocate string buffer on the heap
    stringToUTF8(str, buffer, bufferSize); // fill the buffer with the string UTF-8 value
    return buffer; // return the pointer of the allocated string to C#
  },
    
});