# SportTime - Full-Stack Task Management Application

SportTime - bu zamonaviy vazifalar boshqaruvi ilovasi bo'lib, C# ASP.NET Core backend va Angular frontend bilan yaratilgan.

## 🚀 Xususiyatlar

### Backend (C# ASP.NET Core)
- **RESTful API** - To'liq CRUD operatsiyalari
- **JWT Authentication** - Xavfsiz autentifikatsiya
- **Entity Framework Core** - Ma'lumotlar bazasi bilan ishlash
- **AutoMapper** - Obyektlarni mapping qilish
- **FluentValidation** - Ma'lumotlarni tekshirish
- **Serilog** - Loglarni yozish
- **Swagger/OpenAPI** - API hujjatlari

### Frontend (Angular)
- **Angular 17** - Zamonaviy frontend framework
- **Reactive Forms** - Formalar bilan ishlash
- **HTTP Interceptors** - So'rovlarni boshqarish
- **Route Guards** - Sahifalarni himoya qilish
- **Responsive Design** - Barcha qurilmalarda ishlaydi
- **Modern UI/UX** - Chiroyli va intuitiv interfeys

### Vazifalar boshqaruvi
- ✅ Vazifalarni yaratish, tahrirlash, o'chirish
- 📊 Vazifalar statistikasi
- 🔍 Qidirish va filtrlash
- 📅 Muddat belgilash
- 🏷️ Kategoriyalar va muhimlik darajalari
- ✔️ Vazifalarni bajarildi deb belgilash

## 🛠️ Texnologiyalar

### Backend
- C# 12
- ASP.NET Core 8.0
- Entity Framework Core 8.0
- SQL Server / In-Memory Database
- JWT Bearer Authentication
- AutoMapper
- FluentValidation
- Serilog

### Frontend
- Angular 17
- TypeScript 5
- RxJS
- Angular Material (ixtiyoriy)
- SCSS

## 📦 O'rnatish va ishga tushirish

### Talablar
- .NET 8.0 SDK
- Node.js 18+ va npm
- Visual Studio Code yoki Visual Studio

### 1. Loyihani klonlash
```bash
git clone <repository-url>
cd sporttime-app
```

### 2. Backend'ni ishga tushirish
```bash
# Backend papkasiga o'tish
cd backend

# Paketlarni o'rnatish
dotnet restore

# Ma'lumotlar bazasini yaratish
dotnet ef database update

# Serverni ishga tushirish
dotnet run
```

Backend http://localhost:5000 da ishga tushadi.

### 3. Frontend'ni ishga tushirish
```bash
# Frontend papkasiga o'tish
cd frontend

# Paketlarni o'rnatish
npm install

# Development serverni ishga tushirish
ng serve
```

Frontend http://localhost:4200 da ishga tushadi.

### 4. Bir vaqtda ikkala serverni ishga tushirish
```bash
# Asosiy papkada
npm install
npm run dev
```

## 🔐 Test foydalanuvchisi

Ilovani sinab ko'rish uchun quyidagi ma'lumotlardan foydalaning:

- **Email:** test@sporttime.com
- **Parol:** Test123!

## 📱 Foydalanish

1. **Ro'yxatdan o'tish/Tizimga kirish** - Yangi hisob yarating yoki mavjud hisob bilan kiring
2. **Dashboard** - Barcha vazifalaringizni ko'ring va boshqaring
3. **Yangi vazifa** - "+" tugmasini bosib yangi vazifa qo'shing
4. **Filtrlash** - Kategoriya, muhimlik darajasi yoki holat bo'yicha filtrlang
5. **Qidirish** - Vazifa nomi yoki tavsifi bo'yicha qidiring
6. **Tahrirlash** - Vazifani tahrirlash uchun qalam belgisini bosing
7. **O'chirish** - Vazifani o'chirish uchun axlat qutisi belgisini bosing

## 🏗️ Loyiha tuzilishi

```
sporttime-app/
├── backend/                 # C# ASP.NET Core API
│   ├── Controllers/         # API kontrollerlar
│   ├── Models/             # Ma'lumotlar modellari
│   ├── DTOs/               # Data Transfer Objects
│   ├── Services/           # Biznes logika
│   ├── Repositories/       # Ma'lumotlar bazasi bilan ishlash
│   ├── Data/               # DbContext va seed ma'lumotlar
│   └── Program.cs          # Asosiy konfiguratsiya
├── frontend/               # Angular ilovasi
│   ├── src/
│   │   ├── app/
│   │   │   ├── components/ # Angular komponentlar
│   │   │   ├── services/   # HTTP servislar
│   │   │   ├── models/     # TypeScript modellari
│   │   │   ├── guards/     # Route guards
│   │   │   └── interceptors/ # HTTP interceptors
│   │   └── environments/   # Muhit konfiguratsiyalari
└── package.json           # Asosiy npm skriptlar
```

## 🔧 Konfiguratsiya

### Backend konfiguratsiyasi
`backend/appsettings.json` faylida quyidagi sozlamalarni o'zgartirish mumkin:
- Ma'lumotlar bazasi ulanishi
- JWT sozlamalari
- CORS sozlamalari

### Frontend konfiguratsiyasi
`frontend/src/environments/` papkasida API URL va boshqa sozlamalarni o'zgartirish mumkin.

## 🚀 Production'ga deploy qilish

### Backend
```bash
cd backend
dotnet publish -c Release -o ./publish
```

### Frontend
```bash
cd frontend
ng build --prod
```

## 🤝 Hissa qo'shish

1. Fork qiling
2. Feature branch yarating (`git checkout -b feature/AmazingFeature`)
3. O'zgarishlarni commit qiling (`git commit -m 'Add some AmazingFeature'`)
4. Branch'ni push qiling (`git push origin feature/AmazingFeature`)
5. Pull Request oching

## 📄 Litsenziya

Bu loyiha MIT litsenziyasi ostida tarqatiladi.

## 📞 Aloqa

Savollar yoki takliflar bo'lsa, iltimos bog'laning:
- Email: support@sporttime.com
- GitHub Issues: [Issues sahifasi](https://github.com/your-repo/issues)

---

**SportTime** - Vazifalaringizni samarali boshqaring! 🎯