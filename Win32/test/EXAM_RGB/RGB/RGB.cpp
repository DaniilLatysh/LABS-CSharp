// RGB.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "RGB.h"

#pragma data_seg(".ASHARE")
RGB_API int R = 0;
RGB_API int G = 0;
RGB_API int B = 0;
#pragma data_seg()

#pragma comment(linker, "/SECTION:.ASHARE,RWS")

// This is an example of an exported variable
RGB_API int nRGB = 0;

// This is an example of an exported function.
RGB_API int fnRGB(void)
{
    return 42;
}

// This is the constructor of a class that has been exported.
// see RGB.h for the class definition
CRGB::CRGB()
{
    return;
}
