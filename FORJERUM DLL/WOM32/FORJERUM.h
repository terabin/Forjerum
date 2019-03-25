#ifndef UNICODE
#define UNICODE 1
#endif

#pragma comment(lib,"Ws2_32.lib")
#pragma comment(lib, "user32.lib" )
#pragma comment (lib, "advapi32")
#pragma comment(lib, "crypt32.lib")

#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdio.h>
#include <stdlib.h>
#include <windows.h>
#include <iostream>
#include <tchar.h>
#include <wincrypt.h>
#include <conio.h>
#include <strsafe.h>

#define KEYLENGTH  0x00800000
#define ENCRYPT_ALGORITHM CALG_RC4 
#define ENCRYPT_BLOCK_SIZE 8 

using namespace std;

extern "C"
{
  int __declspec(dllexport) womsocket(int family, int type, int protocol)
  {
	//SOCKET VAR
    SOCKET sock = (family, type, protocol);

	//SOCKET
	sock = socket(family, type, protocol);

	//RETORNAR
	return sock;
  }
  int __declspec(dllexport) womconnect(SOCKET sock, char *ip, int port)
  {	
	//RESULTADO FINAL
	int iResult = 0;

	//Resolutação DNS
    struct hostent        *he;
    
	//RESOLVER DNS
    if ( (he = gethostbyname(ip) ) == NULL ) {
      return 0; /* error */
    }

    //NOVO ADDR APTO PARA RESOLVER DNS
    sockaddr_in clientService;
	memcpy(&clientService.sin_addr, he->h_addr_list[0], he->h_length);
    clientService.sin_family = AF_INET;
    clientService.sin_port = htons(port);


    //ADDR
    //sockaddr_in clientService;
    //clientService.sin_family = AF_INET;
    //clientService.sin_addr.s_addr = inet_addr(ip);
    //clientService.sin_port = htons(port);

    //CONECTAR
    iResult = connect(sock, (SOCKADDR *) & clientService, sizeof (clientService));
    if (iResult == SOCKET_ERROR) {
        return 1;
    }

	return 0;
  }
  int __declspec(dllexport) womsockopt(SOCKET sock, int protocol, int opt, bool val, int size)
  {
	//RESULTADO FINAL
    int iResult = 0;

	//VALORES
    BOOL bOptVal = val;
    int bOptLen = sizeof (BOOL);

	//ALTERAR CONFIGURAÇÃO
    iResult = setsockopt(sock, protocol, opt, (char *) &bOptVal, bOptLen);

	//RETORNAR PROCESSO
	return iResult;
  }
  int __declspec(dllexport) womtick()
  {
	return GetTickCount();
  }
  int __declspec(dllexport) womsend(SOCKET sock, char *sendbuf, int flags)
  {
	//RESULTADO FINAL
	int iResult = 0;

	//ENVIAR DADOS
	iResult = send(sock, sendbuf, (int)strlen(sendbuf), flags);

	//RETORNAR PROCESSO
	return iResult;
  }
  int __declspec(dllexport) womrecv(SOCKET sock, char *buf, int size, int flags)
  {
	//RESULTADO FINAL
	int iResult = 0;

	//RECEBER DADOS
	iResult = recv(sock, buf, size, flags);

	//RETORNAR PROCESSO
	return iResult;
  }
  int __declspec(dllexport) womclose(SOCKET sock)
  {
	//RESULTADO FINAL
	int iResult = 0;

	//RECEBER DADOS
	iResult = closesocket(sock);

	//RETORNAR PROCESSO
	return iResult;
  }
  u_long __declspec(dllexport) womavailable(SOCKET sock, long opt)
  {
	//RESULTADO FINAL
	int iResult = 0;
	u_long iMode = 0;

	//RECEBER DADOS
	iResult = ioctlsocket(sock, opt, &iMode);

	//RETORNAR PROCESSO
	return iMode;
  }
  HWND __declspec(dllexport) womwindow()
  {
	//CLASSE
	char const *rgss = "RGSS Player";

	//RECEBER DADOS
    HWND hwnds = FindWindowA(rgss, 0); 

	//RETORNAR PROCESSO
	return hwnds;
  }
  int __declspec(dllexport) womshowcursor(bool opt)
  {
	//Alterar cursor
    int iResult = ShowCursor(opt);

	//RETORNAR PROCESSO
	return iResult;
  }

  char __declspec(dllexport) womscreen()
  {
	//string *fullscreen_code = "test"
	  return 0;
	//RETORNAR PROCESSO
	//return *fullscreen_code;
  }
}
