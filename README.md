# ASP.NET Core & MySQL Code Samples
Repository ini berisi contoh-contoh aplikasi web yang dibangun dengan ASP.NET Core untuk mengakses database MySQL.  Setiap sebuah contoh aplikasi web akan disimpan di dalam sebuah folder.  Kode program pada repository ini ditulis dengan menggunakan Visual Studio Code.  
Berikut adalah daftar contoh aplikasi web yang ada di sini:
- MyCoreGuestBook
- EFCoreGuestBook - Database First
- EFCoreBookStore

## MyCoreGuestBook
MyCoreGuestBook adalah aplikasi web super sangat sederhana yang berfungsi untuk menampilkan daftar pengisi buku tamu dan form untuk mengisi buku tamu.  Aplikasi ini memberikan contoh cara mengakses database MySQL dengan library MySQL.Data.Core.

## EFCoreGuestBook - Database First
EFCoreGuestBook adalah aplikasi web yang lumayan sangat sederhana yang berfungsi untuk menampilkan daftar pengisi buku tamu dan form untuk mengisi buku tamu.  Aplikasi ini memberikan contoh cara mengakses database MySQL dengan Entity Framework dengan menggunakan Provider MySQL database. Pendekatan yang digunakan pada project ini adalah Database First, artinya database dan tabel-tabel telah dipersiapkan terlebih dahulu.

## EFCoreBookStore
Book store adalah aplikasi web menggunakan ASP.NET Core MVC dan Bootstrap 3 framework untuk antarmuka. Database yang digunakan adalah MySQL. Fitur-fitur aplikasi web ini adalah:
- Mengelola pengarang buku, fitur ini dapat digunakan untuk menampilkan, menambah, mengedit dan menghapus data pengarang buku.
- Mengelola kategori buku, fitur ini dapat digunakan untuk menampilkan, menambah, mengedit dan menghapus data kategori buku.
- Mengelola buku, fitur ini dapat digunakan untuk menampilkan, menambah, mengedit dan menghapus data buku.

## Script SQL
Pada setiap project telah disediakan file script SQL untuk membuat tabel yang diperlukan pada project. 
