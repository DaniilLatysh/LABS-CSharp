// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the CONSOLEAPPLICATION2_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// CONSOLEAPPLICATION2_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef CONSOLEAPPLICATION2_EXPORTS
#define CONSOLEAPPLICATION2_API __declspec(dllexport)
#else
#define CONSOLEAPPLICATION2_API __declspec(dllimport)
#endif

// This class is exported from the ConsoleApplication2.dll
class CONSOLEAPPLICATION2_API CConsoleApplication2 {
public:
	CConsoleApplication2(void);
	// TODO: add your methods here.
};

extern CONSOLEAPPLICATION2_API int nConsoleApplication2;

CONSOLEAPPLICATION2_API int fnConsoleApplication2(void);
