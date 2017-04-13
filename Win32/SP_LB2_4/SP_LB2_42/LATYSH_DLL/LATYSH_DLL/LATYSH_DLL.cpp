// LATYSH_DLL.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "LATYSH_DLL.h"

#pragma data_seg(".ASHARE")
int g_nDllCallsCount = 0;
#pragma data_seg()
#pragma comment(linker, "/SECTION:.ASHARE,RWS")


LATYSH_DLL_API double Fun91(int x, int y)
{
	return (double)x / y;
}

LATYSH_DLL_API int WINAPI Fun92(int x, int y, int z)
{

	return (int)y + x + z;

}

LATYSH_DLL_API void Fun93(double in, double *out)
{
	double s = in + 5;
	*out = s;
}