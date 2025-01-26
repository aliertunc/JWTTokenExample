# Authentication Service API

Bu API, kullanıcılara **JWT (JSON Web Token)** tabanlı kimlik doğrulama ve yetkilendirme işlemleri sunar. Kullanıcılar, giriş yaparak **access token** ve **refresh token** alabilir. Ayrıca, refresh token ile yeni access token alabilir ve çıkış yapabilirler.

## Özellikler

- **Login**: Kullanıcı adı ve şifre ile giriş yaparak **access token** ve **refresh token** alabilirsiniz.
- **Refresh Token**: Geçerli bir refresh token ile yeni bir access token alabilirsiniz.
- **Logout**: Refresh token iptal edilerek çıkış yapılabilir.
- **Authorization**: API, kullanıcı rolleriyle erişim kontrolü sağlar (Admin, User).

## API Sonuçları

1. **Login Endpoint (Kullanıcı Girişi)**  
   - **URL**: `/api/auth/login`  
   - **Method**: `POST`
   - **Body**:
     ```json
     {
       "role": "Admin" // "Admin" veya "User" olmalı
     }
     ```
   - **Response**:
     ```json
     {
       "AccessToken": "access_token_here",
       "RefreshToken": "refresh_token_here"
     }
     ```
   
2. **Refresh Token Endpoint**  
   - **URL**: `/api/auth/refresh-token`  
   - **Method**: `POST`
   - **Body**:
     ```json
     {
       "RefreshToken": "refresh_token_here"
     }
     ```
   - **Response**:
     ```json
     {
       "AccessToken": "new_access_token_here",
       "RefreshToken": "new_refresh_token_here"
     }
     ```
   
3. **Logout Endpoint**  
   - **URL**: `/api/auth/logout`  
   - **Method**: `POST`
   - **Body**:
     ```json
     {
       "RefreshToken": "refresh_token_here"
     }
     ```
   - **Response**:
     ```json
     {
       "Message": "Çıkış yapıldı"
     }
     ```

## Kullanıcı Yetkilendirme

- `Admin` rolüyle erişim:
  - **URL**: `/api/auth/admin/post`  
  - **Method**: `POST`
  - **Authorization**: Bearer token (Admin role)
  - **Response**:
    ```json
    {
      "message": "Admin tarafından post işlemi başarılı!"
    }
    ```

- `User` rolüyle erişim:
  - **URL**: `/api/auth/user/post`  
  - **Method**: `POST`
  - **Authorization**: Bearer token (User role)
  - **Response**:
    ```json
    {
      "message": "User tarafından post işlemi başarılı!"
    }
    ```

## Teknolojiler

- **.NET 9.0**
- **JWT (JSON Web Token)**: Kimlik doğrulama ve yetkilendirme
- **Role-Based Authorization**: Kullanıcıların rollere göre yetkilendirilmesi
- **C# / ASP.NET Core Web API**

## Kurulum

### 1. Projeyi Klonlayın

```bash
git clone https://github.com/kullaniciadi/projeniz.git
cd projeniz
