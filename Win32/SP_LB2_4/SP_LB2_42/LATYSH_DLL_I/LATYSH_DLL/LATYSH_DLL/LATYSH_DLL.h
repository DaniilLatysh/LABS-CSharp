
#ifdef LATYSH_DLL_EXPORTS
#define LATYSH_DLL_API __declspec(dllexport)
#else
#define LATYSH_DLL_API __declspec(dllimport)
#endif


LATYSH_DLL_API FILE* OpenFile(LPCTSTR);
LATYSH_DLL_API void SearchStringFile(FILE*);