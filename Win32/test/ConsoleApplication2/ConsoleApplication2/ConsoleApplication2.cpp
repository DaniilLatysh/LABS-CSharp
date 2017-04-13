// ConsoleApplication2.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "ConsoleApplication2.h"


// This is an example of an exported variable
CONSOLEAPPLICATION2_API int nConsoleApplication2=0;

// This is an example of an exported function.
CONSOLEAPPLICATION2_API int fnConsoleApplication2(void)
{
    return 42;
}

// This is the constructor of a class that has been exported.
// see ConsoleApplication2.h for the class definition
CConsoleApplication2::CConsoleApplication2()
{
    return;
}
