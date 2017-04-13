// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"

BOOL WINAPI DllMain(HMODULE hModule,
					DWORD  ul_reason_for_call,
					LPVOID lpReserved
)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	{
		MessageBox(NULL, L"Load library LATYSH_DLL. Project Latysh D.N.",
			L"Message", MB_OK);
	}
	break;
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
	{
		MessageBox(NULL, L"Unload library LATYSH_DLL. Project Latysh D.N.",
			L"Message", MB_OK);
		return true;
	}
	break;
	}
	return TRUE;
}


