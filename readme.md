## controller

- Một lớp C# do bạn định nghĩa kế thừa từ lớp Microsoft.AspNetCore.Mvc.Controller, khi các truy vấn đến ứng dụng tùy thuộc vào cấu hình, căn cứ vào URL truy cập mà controller được khởi tạo và thiết lập vào nó (trong các property) tất cả thông tin của một reques, và một phương thức trong controller (một action) được thực thi để xử lý truy vấn.

## Action:

- Các action là những phương thức public (không là static, không overloaded) trong controller được gọi tự động tùy thuộc vào sự điều hướng của route trong ứng dụng (căn cứ vào URL).

// services.AddSingleton -> tao ra doi tuong dich vu
// services.AddTransient -> moi lan truy van mot doi tuong moi duoc tao ra
// services.AddScoped -> moi phien truy cap neu lay dich vu nay ra thi mot doi tuong moi duoc tao ra

## View:

Là các file .cshtml tích hợp sẵn cú pháp Razor (razor engine), (hãy xem về Razor Page để biết cú pháp viết trong view .cshtml) được tổ chức thành các thư mục cho từng Controller. Nếu controller tên là Home thì các view đặt trong thự mục /View/Home, tên các file view tương ứng tên Action của Controller, qua đó nó được dùng để dựng HTML.

## Truyen du lieu sang view

-Model
-ViewData
-ViewBag
-TempData

## Area

- La ten dung de routing
- La cau truc thu muc MVC
- Thiet lap area cho controller bang `[Area("AreaName")]`
- Tao cau truc thu muc `dotnet aspnet-codegenerator area NameArea`
