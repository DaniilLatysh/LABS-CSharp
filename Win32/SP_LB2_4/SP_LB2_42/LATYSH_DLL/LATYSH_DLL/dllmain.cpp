// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"
#include "LATYSH_DLL.h"

BOOL APIENTRY DllMain(HMODULE hModule,
	DWORD  ul_reason_for_call,
	LPVOID lpReserved
)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	{
		MessageBox(NULL, L"Loading library LATYSH_DLL. Project Latysh D.N.",
			L"MESSAGE", MB_OK);
		g_nDllCallsCount++;
		return true;
	}
	case DLL_THREAD_ATTACH:
	{
		MessageBox(NULL, L"THREAD START", L"MESSAGE", MB_OK);
		break;
	}
	case DLL_THREAD_DETACH:
	{
		MessageBox(NULL, L"THREAD WAS DESTROY", L"MESSAGE", MB_OK);
		break;
	}
	case DLL_PROCESS_DETACH:
	{
		MessageBox(NULL, L"Unloading library LATYSH_DLL. Latysh D.N.",
			L"MESSAGE", MB_OK);
		g_nDllCallsCount--;
		return true;
	}
	break;
	}
	return TRUE;
}

