# mustafa-gundogdu-homework-5

*BackgroundWorker oluþturulacak. https://jsonplaceholder.typicode.com/posts bu linketeki her bir dakikada çalýþýp bu bilgileri çekip veri tabanýna kayýt eden bir repository oluþturulacak.
https://drive.google.com/file*/d/17OUbFAua2kTngQLO7FGY-kxwvXLxnDEZ/view?usp=sharing bunun üstüne oluþturabilirsin. Post diye tablo oluþturulacak migration ile user, id, title, body postun kolonlarý olacak.

#Steps
*1-> First.App.Domain katmanýnda Post class'ý oluþturulup BaseEntity den miras aldýk 
*2-> First.App.EntityFramework katmanýnda configurations klasöründe PostConfiguration oluþturuldu ve appDbContext e dbset eklendi.
*3-> First.App.Business katmanýnda Post iþlemleri yapacak olan interface ve service classý oluþturuldu
*4-> Kuyruk yapýsý için First.App.Business katmanýnda IBackgroundQueue ve BackgroundQueue eklendi ve kuyruk yapýsý generic olarak oluþturuldu
*5-> PostAPI oluþturuldu ve Background klasörü oluþturuldu
*6-> Öncelikle her 60 saniyede bir çalýþacak PostBackgroundCallWorker oluþturuldu bu sýnýfta gelen postlar kuyruða eklendi
*7-> Kuyruktaki verileri database e eklemek için ise PostBackgroundSaveWorker oluþturuldu kuyruktan FIFO mantýðý ile sýrayla ekleme iþlemi yapýldý
*8-> PostController ile veritabanýndaki postlarý görüntüleme iþlemleri yapýldý...