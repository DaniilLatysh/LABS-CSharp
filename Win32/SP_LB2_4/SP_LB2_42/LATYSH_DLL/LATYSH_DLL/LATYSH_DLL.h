
#ifdef LATYSH_DLL_EXPORTS

#define LATYSH_DLL_API extern "C" __declspec(dllexport)
#else

#define LATYSH_DLL_API extern "C" __declspec(dllimport)
#endif



LATYSH_DLL_API int g_nDllCallsCount;

LATYSH_DLL_API int g_nFnCallsCount;


LATYSH_DLL_API double Fun91(int, int);

LATYSH_DLL_API int WINAPI Fun92(int, int, int);

LATYSH_DLL_API void Fun93(double, double*);