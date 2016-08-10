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
  bool MyEncryptFile(
    LPTSTR szSource, 
    LPTSTR szDestination, 
    LPTSTR szPassword);
  bool MyDecryptFile(
    LPTSTR szSource, 
    LPTSTR szDestination, 
    LPTSTR szPassword);
  char __declspec(dllexport) womencrypt(wchar_t* filedir)
  {
	 TCHAR NPath[MAX_PATH];
     GetCurrentDirectory(MAX_PATH, NPath);

     TCHAR pszSource[260] = _T("");
     StringCchCat(pszSource, 260, NPath);  
     StringCchCat(pszSource, 260, filedir);
	 StringCchCat(pszSource, 260, _T(".png"));

	 TCHAR pszDestination[260] = _T("");
     StringCchCat(pszDestination, 260, NPath);  
     StringCchCat(pszDestination, 260, filedir);
	 StringCchCat(pszDestination, 260, _T(".wing"));

     LPTSTR pszPassword = L"0010WING0101";

	 if(MyEncryptFile(pszSource, pszDestination, pszPassword))
     {
       return 0;
     }
     else
     {
       return 1;
     }

	 return 1;
  }
  char __declspec(dllexport) womdecrypt(wchar_t* filedir)
  {
	 TCHAR NPath[MAX_PATH];
     GetCurrentDirectory(MAX_PATH, NPath);

     TCHAR pszSource[260] = _T("");
     StringCchCat(pszSource, 260, NPath);  
     StringCchCat(pszSource, 260, filedir);
	 StringCchCat(pszSource, 260, _T(".wing"));

	 TCHAR pszDestination[260] = _T("");
     StringCchCat(pszDestination, 260, NPath);  
     StringCchCat(pszDestination, 260, filedir);
	 StringCchCat(pszDestination, 260, _T(".png"));

     LPTSTR pszPassword = L"0010WING0101";

	 if(MyDecryptFile(pszSource, pszDestination, pszPassword))
	 {
		  return 0;
	 }
	 else
	 {
		  return 1;
	 }

	 return 1;
  }
    char __declspec(dllexport) womrvdata2en(wchar_t* filedir)
  {
	 TCHAR NPath[MAX_PATH];
     GetCurrentDirectory(MAX_PATH, NPath);

     TCHAR pszSource[260] = _T("");
     StringCchCat(pszSource, 260, NPath);  
     StringCchCat(pszSource, 260, filedir);
	 StringCchCat(pszSource, 260, _T(".rvdata2"));

	 TCHAR pszDestination[260] = _T("");
     StringCchCat(pszDestination, 260, NPath);  
     StringCchCat(pszDestination, 260, filedir);
	 StringCchCat(pszDestination, 260, _T(".wing"));

     LPTSTR pszPassword = L"0010WING0101";

	 if(MyEncryptFile(pszSource, pszDestination, pszPassword))
     {
       return 0;
     }
     else
     {
       return 1;
     }

	 return 1;
  }
  char __declspec(dllexport) womrvdata2de(wchar_t* filedir)
  {
	 TCHAR NPath[MAX_PATH];
     GetCurrentDirectory(MAX_PATH, NPath);

     TCHAR pszSource[260] = _T("");
     StringCchCat(pszSource, 260, NPath);  
     StringCchCat(pszSource, 260, filedir);
	 StringCchCat(pszSource, 260, _T(".wing"));

	 TCHAR pszDestination[260] = _T("");
     StringCchCat(pszDestination, 260, NPath);  
     StringCchCat(pszDestination, 260, filedir);
	 StringCchCat(pszDestination, 260, _T(".rvdata2"));

     LPTSTR pszPassword = L"0010WING0101";

	 if(MyDecryptFile(pszSource, pszDestination, pszPassword))
	 {
		  return 0;
	 }
	 else
	 {
		  return 1;
	 }

	 return 1;
  }

bool MyDecryptFile(
    LPTSTR pszSourceFile, 
    LPTSTR pszDestinationFile, 
    LPTSTR pszPassword)
{ 

    bool fReturn = false;
    HANDLE hSourceFile = INVALID_HANDLE_VALUE;
    HANDLE hDestinationFile = INVALID_HANDLE_VALUE; 
    HCRYPTKEY hKey = NULL; 
    HCRYPTHASH hHash = NULL; 

    HCRYPTPROV hCryptProv = NULL; 

    DWORD dwCount;
    PBYTE pbBuffer = NULL; 
    DWORD dwBlockLen; 
    DWORD dwBufferLen; 


    hSourceFile = CreateFile(
        pszSourceFile, 
        FILE_READ_DATA,
        FILE_SHARE_READ,
        NULL,
        OPEN_EXISTING,
        FILE_ATTRIBUTE_NORMAL,
        NULL);
    if(INVALID_HANDLE_VALUE != hSourceFile)
    {

    }
    else
    { 
        goto Exit_MyDecryptFile;
    } 


    hDestinationFile = CreateFile(
        pszDestinationFile, 
        FILE_WRITE_DATA,
        FILE_SHARE_READ,
        NULL,
        OPEN_ALWAYS,
        FILE_ATTRIBUTE_NORMAL,
        NULL);
    if(INVALID_HANDLE_VALUE != hDestinationFile)
    {

    }
    else
    {
		MessageBox(NULL, TEXT("Erro ao processar memória de destino. Código 20"), TEXT("Erro"), 0);
        goto Exit_MyDecryptFile;
    }

    if(CryptAcquireContext(
        &hCryptProv, 
        NULL, 
        MS_ENHANCED_PROV, 
        PROV_RSA_FULL, 
        0))
    {

    }
    else
    {
		if (GetLastError() == NTE_BAD_KEYSET)
		{
			if (!CryptAcquireContext(
				&hCryptProv, 
				NULL, 
				MS_ENHANCED_PROV, 
				PROV_RSA_FULL, 
				CRYPT_NEWKEYSET))
			{
				MessageBox(NULL, TEXT("Erro durante CryptAcquireContext. Código 21"), TEXT("Erro"), 0);
                goto Exit_MyDecryptFile;
			}
		}
    }


    if(!pszPassword || !pszPassword[0]) 
    { 


        DWORD dwKeyBlobLen;
        PBYTE pbKeyBlob = NULL;
 
        if(!ReadFile(
            hSourceFile, 
            &dwKeyBlobLen, 
            sizeof(DWORD), 
            &dwCount, 
            NULL))
        {
			MessageBox(NULL, TEXT("Erro durante leitura de memória. Código 22"), TEXT("Erro"), 0);
            goto Exit_MyDecryptFile;
        }

        if(!(pbKeyBlob = (PBYTE)malloc(dwKeyBlobLen)))
        {

        }

 
        if(!ReadFile(
            hSourceFile, 
            pbKeyBlob, 
            dwKeyBlobLen, 
            &dwCount, 
            NULL))
        {
			MessageBox(NULL, TEXT("Erro ao ler memória de origem. Código 23"), TEXT("Erro"), 0);
            goto Exit_MyDecryptFile;
        }

 
        if(!CryptImportKey(
              hCryptProv, 
              pbKeyBlob, 
              dwKeyBlobLen, 
              0, 
              0, 
              &hKey))
        {
			MessageBox(NULL, TEXT("Erro durante CryptImportKey. Código 24"), TEXT("Erro"), 0);
            goto Exit_MyDecryptFile;
        }

        if(pbKeyBlob)
        {
            free(pbKeyBlob);
        }
    }
    else
    {
 
        if(!CryptCreateHash(
               hCryptProv, 
               CALG_MD5, 
               0, 
               0, 
               &hHash))
        {
			MessageBox(NULL, TEXT("Erro durante CryptCreateHash. Código 25"), TEXT("Erro"), 0);
            goto Exit_MyDecryptFile;
        }
 
        if(!CryptHashData(
               hHash, 
               (BYTE *)pszPassword, 
               lstrlen(pszPassword), 
               0)) 
        {
			MessageBox(NULL, TEXT("Erro durante CryptHashData. Código 26"), TEXT("Erro"), 0);
            goto Exit_MyDecryptFile;
        }
    

        if(!CryptDeriveKey(
              hCryptProv, 
              ENCRYPT_ALGORITHM, 
              hHash, 
              KEYLENGTH, 
              &hKey))
        { 
			MessageBox(NULL, TEXT("Erro durante CryptDeriveKey. Código 27"), TEXT("Erro"), 0);
            goto Exit_MyDecryptFile;
        }
    }


    dwBlockLen = 1000 - 1000 % ENCRYPT_BLOCK_SIZE; 
    dwBufferLen = dwBlockLen; 


    if(!(pbBuffer = (PBYTE)malloc(dwBufferLen)))
    {
	   MessageBox(NULL, TEXT("Erro ao alocar memória. Código 28"), TEXT("Erro"), 0);
       goto Exit_MyDecryptFile;
    }
    

    bool fEOF = false;
    do
    {

        if(!ReadFile(
            hSourceFile, 
            pbBuffer, 
            dwBlockLen, 
            &dwCount, 
            NULL))
        {
			MessageBox(NULL, TEXT("Erro ao processar memória de origem. Código 29"), TEXT("Erro"), 0);;
            goto Exit_MyDecryptFile;
        }

        if(dwCount < dwBlockLen)
        {
            fEOF = TRUE;
        }

        if(!CryptDecrypt(
              hKey, 
              0, 
              fEOF, 
              0, 
              pbBuffer, 
              &dwCount))
        {
			MessageBox(NULL, TEXT("Erro durante CryptDecrypt. Código 30"), TEXT("Erro"), 0);
            goto Exit_MyDecryptFile;
        }

        if(!WriteFile(
            hDestinationFile, 
            pbBuffer, 
            dwCount,
            &dwCount,
            NULL))
        { 
			MessageBox(NULL, TEXT("Erro ao criar memória virtual. Código 31"), TEXT("Erro"), 0);
            goto Exit_MyDecryptFile;
        }

    }while(!fEOF);

    fReturn = true;

Exit_MyDecryptFile:


    if(pbBuffer)
    {
        free(pbBuffer);
    }

    if(hSourceFile)
    {
        CloseHandle(hSourceFile);
    }

    if(hDestinationFile)
    {
        CloseHandle(hDestinationFile);
    }


    if(hHash) 
    {
        if(!(CryptDestroyHash(hHash)))
        {
			MessageBox(NULL, TEXT("Erro ao liberar memória. Código 33"), TEXT("Erro"), 0);
        }

        hHash = NULL;
    }

 
    if(hKey)
    {
        if(!(CryptDestroyKey(hKey)))
        {
			MessageBox(NULL, TEXT("Erro ao liberar memória. Código 32"), TEXT("Erro"), 0);
        }
    } 
 
    if(hCryptProv)
    {
        if(!(CryptReleaseContext(hCryptProv, 0)))
        {
			MessageBox(NULL, TEXT("Erro durante CryptReleaseContext. Código 31"), TEXT("Erro"), 0);
        }
    } 

    return fReturn;
}


bool MyEncryptFile(
    LPTSTR pszSourceFile, 
    LPTSTR pszDestinationFile, 
    LPTSTR pszPassword)
{ 

    bool fReturn = false;
    HANDLE hSourceFile = INVALID_HANDLE_VALUE;
    HANDLE hDestinationFile = INVALID_HANDLE_VALUE; 

    HCRYPTPROV hCryptProv = NULL; 
    HCRYPTKEY hKey = NULL; 
    HCRYPTKEY hXchgKey = NULL; 
    HCRYPTHASH hHash = NULL; 

    PBYTE pbKeyBlob = NULL; 
    DWORD dwKeyBlobLen; 

    PBYTE pbBuffer = NULL; 
    DWORD dwBlockLen; 
    DWORD dwBufferLen; 
    DWORD dwCount; 
     

    hSourceFile = CreateFile(
        pszSourceFile, 
        FILE_READ_DATA,
        FILE_SHARE_READ,
        NULL,
        OPEN_EXISTING,
        FILE_ATTRIBUTE_NORMAL,
        NULL);
    if(INVALID_HANDLE_VALUE != hSourceFile)
    {
    }
    else
    { 
        	MessageBox(NULL, TEXT("Erro ao abrir memória plana. Código: 2"), TEXT("Erro"), 0);
        goto Exit_MyEncryptFile;
    } 


    hDestinationFile = CreateFile(
        pszDestinationFile, 
        FILE_WRITE_DATA,
        FILE_SHARE_READ,
        NULL,
        OPEN_ALWAYS,
        FILE_ATTRIBUTE_NORMAL,
        NULL);
    if(INVALID_HANDLE_VALUE != hDestinationFile)
    {
    }
    else
    {
        MessageBox(NULL, TEXT("Erro ao encontrar referência de memória. Código: 3"), TEXT("Erro"), 0);
        goto Exit_MyEncryptFile;
    }
 
    if(CryptAcquireContext(
        &hCryptProv, 
        NULL, 
        MS_ENHANCED_PROV, 
        PROV_RSA_FULL, 
        0)) 
    {
    }
    else
    {
		if (GetLastError() == NTE_BAD_KEYSET)
		{
			if (!CryptAcquireContext(
				&hCryptProv, 
				NULL, 
				MS_ENHANCED_PROV, 
				PROV_RSA_FULL, 
				CRYPT_NEWKEYSET))
			{
				MessageBox(NULL, TEXT("Erro durante CryptAcquireContext. Código 4"), TEXT("Erro"), 0);
				goto Exit_MyEncryptFile;
			}
		}
    }

    if(!pszPassword || !pszPassword[0]) 
    { 

        if(CryptGenKey(
            hCryptProv, 
            ENCRYPT_ALGORITHM, 
            KEYLENGTH | CRYPT_EXPORTABLE, 
            &hKey))
        {

        } 
        else
        {
            	MessageBox(NULL, TEXT("Erro durante CryptGenKey: Código 5"), TEXT("Erro"), 0);
            goto Exit_MyEncryptFile;
        }


        if(CryptGetUserKey(
            hCryptProv, 
            AT_KEYEXCHANGE, 
            &hXchgKey))
        {

        }
        else
        { 
            if(NTE_NO_KEY == GetLastError())
            {
                if(!CryptGenKey(
                    hCryptProv, 
                    AT_KEYEXCHANGE, 
                    CRYPT_EXPORTABLE, 
                    &hXchgKey))
                {
                    	MessageBox(NULL, TEXT("Falha ao criar chave virtual. Código 6"), TEXT("Erro"), 0);
                    goto Exit_MyEncryptFile;
                }
            }
            else
            {
                	MessageBox(NULL, TEXT("Chave virtual indisponível. Código 6.1"), TEXT("Erro"), 0);
                goto Exit_MyEncryptFile;
            }
        }
 
        if(CryptExportKey(
            hKey, 
            hXchgKey, 
            SIMPLEBLOB, 
            0, 
            NULL, 
            &dwKeyBlobLen))
        {
        }
        else
        {  
            	MessageBox(NULL, TEXT("Erro ao computar distância Blob ;D. Código 7"), TEXT("Erro"), 0);
            goto Exit_MyEncryptFile;
        }

        if(pbKeyBlob = (BYTE *)malloc(dwKeyBlobLen))
        { 

        }
        else
        { 
           	MessageBox(NULL, TEXT("Falta de memória para execução. Código 7.1"), TEXT("Erro"), 0); 
            goto Exit_MyEncryptFile;
        }

        if(CryptExportKey(
            hKey, 
            hXchgKey, 
            SIMPLEBLOB, 
            0, 
            pbKeyBlob, 
            &dwKeyBlobLen))
        {
        } 
        else
        {
            MessageBox(NULL, TEXT("Erro durante CryptExportKey. Código 8"), TEXT("Erro"), 0);
            goto Exit_MyEncryptFile;
        } 

        if(hXchgKey)
        {
            if(!(CryptDestroyKey(hXchgKey)))
            {
                MessageBox(NULL, TEXT("Falha ao liberar espaço. Código 9"), TEXT("Erro"), 0); 
                goto Exit_MyEncryptFile;
            }
      
            hXchgKey = 0;
        }
     
        if(!WriteFile(
            hDestinationFile, 
            &dwKeyBlobLen, 
            sizeof(DWORD),
            &dwCount,
            NULL))
        { 
           	MessageBox(NULL, TEXT("Falha ao tentar criar memória. Código: 1"), TEXT("Erro"), 0);
            goto Exit_MyEncryptFile;
        }
        else
        {
        }


        if(!WriteFile(
            hDestinationFile, 
            pbKeyBlob, 
            dwKeyBlobLen,
            &dwCount,
            NULL))
        { 
           	MessageBox(NULL, TEXT("Falha ao tentar criar memória. Código: 1.1"), TEXT("Erro"), 0);
            goto Exit_MyEncryptFile;
        }
        else
        {
            
        }

        free(pbKeyBlob);
    } 
    else 
    { 

        if(CryptCreateHash(
            hCryptProv, 
            CALG_MD5, 
            0, 
            0, 
            &hHash))
        {

        }
        else
        { 
            MessageBox(NULL, TEXT("Erro durante CryptCreateHash. Código 10"), TEXT("Erro"), 0);
            goto Exit_MyEncryptFile;
        }  


        if(CryptHashData(
            hHash, 
            (BYTE *)pszPassword, 
            lstrlen(pszPassword), 
            0))
        {

        }
        else
        {
            MessageBox(NULL, TEXT("Erro durante CryptHashData. Código 11"), TEXT("Erro"), 0);
        }


        if(CryptDeriveKey(
            hCryptProv, 
            ENCRYPT_ALGORITHM, 
            hHash, 
            KEYLENGTH, 
            &hKey))
        {
            
        }
        else
        {
            MessageBox(NULL, TEXT("Erro durante CryptDeriveKey. Código 12"), TEXT("Erro"), 0);
            goto Exit_MyEncryptFile;
        }
    } 


    dwBlockLen = 1000 - 1000 % ENCRYPT_BLOCK_SIZE; 

    if(ENCRYPT_BLOCK_SIZE > 1) 
    {
        dwBufferLen = dwBlockLen + ENCRYPT_BLOCK_SIZE; 
    }
    else 
    {
        dwBufferLen = dwBlockLen; 
    }
        

    if(pbBuffer = (BYTE *)malloc(dwBufferLen))
    {
    }
    else
    { 
        MessageBox(NULL, TEXT("Falta de memória, impossível continuar. Código 13"), TEXT("Erro"), 0);
        goto Exit_MyEncryptFile;
    }


    bool fEOF = FALSE;
    do 
    { 

        if(!ReadFile(
            hSourceFile, 
            pbBuffer, 
            dwBlockLen, 
            &dwCount, 
            NULL))
        {
           MessageBox(NULL, TEXT("Erro ao ler processar memória plana. Código 14"), TEXT("Erro"), 0);
        }

        if(dwCount < dwBlockLen)
        {
			fEOF = TRUE;
        }


        if(!CryptEncrypt(
            hKey, 
            NULL, 
            fEOF,
            0, 
            pbBuffer, 
            &dwCount, 
            dwBufferLen))
        { 
            MessageBox(NULL, TEXT("Erro durante CryptEncrypt. Código 15"), TEXT("Erro"), 0);
            goto Exit_MyEncryptFile;
        } 


        if(!WriteFile(
            hDestinationFile, 
            pbBuffer, 
            dwCount,
            &dwCount,
            NULL))
        { 
            MessageBox(NULL, TEXT("Erro ao criar CipherText. Código 16"), TEXT("Erro"), 0);
            goto Exit_MyEncryptFile;
        }

    } while(!fEOF);

    fReturn = true;

Exit_MyEncryptFile:

    if(hSourceFile)
    {
        CloseHandle(hSourceFile);
    }

    if(hDestinationFile)
    {
        CloseHandle(hDestinationFile);
    }


    if(pbBuffer) 
    {
        free(pbBuffer); 
    }
     

    if(hHash) 
    {
        if(!(CryptDestroyHash(hHash)))
        {
            	MessageBox(NULL, TEXT("Erro durante CryptDestroyHash. Código 17"), TEXT("Erro"), 0);
        }

        hHash = NULL;
    }

    if(hKey)
    {
        if(!(CryptDestroyKey(hKey)))
        {
	       MessageBox(NULL, TEXT("Erro durante CryptDestroyKey. Código 18"), TEXT("Erro"), 0);
        }
    }


    if(hCryptProv)
    {
        if(!(CryptReleaseContext(hCryptProv, 0)))
        {
		    MessageBox(NULL, TEXT("Erro durante CryptReleaseContext. Código 19"), TEXT("Erro"), 0);
        }
    }
    
    return fReturn; 
} 

}
