# Помогалка главам сфер ШРК
<b>Версия 1.2</b>

Ахой! Эта программа предназначена для подсчета отчетов деятельности Охранной, Продовольственной, Врачевательной сфер ШРК. Для нее используются отчеты:
- пограничные патрули
- охоты (не одиночные)
- веточники/травники/мховники
- одиночные веточники/травники/мховники
- докторские патрули

Данная програма пока не умеет находить ошибки в отчетах, поэтому перепроверяйте отчеты вручную!

Чтобы начать пользоваться программой, скачайте данный репозиторий, идите в папку app2\bin\Debug\net6.0-windows. Там будет файл "helper_shrk.exe", который и запустите! И пожалуйста, не убирайте из этой папки остальные файлы, они нужны для работы экзешника.

# Как пользоваться?
1. При запуске программы вам нужно выбрать нужную вам сферу. Обязательно проверяйте флажки, поскольку отмеченная сфера неверно преобразует отчеты другой сферы.
2. Далее копируйте указанные выше отчеты из блогов для дальнейшей обработки (можно даже не чистить от мусора, поскольку программа увидит только нужные ей строки). Главное уберите лишние отчеты, не подходящие для обработки, иначе выйдут ошибки в выводе!
3. Жмете кнопку "Начать подсчет", чтобы запустить обработку отчетов. Выходящие данные отобразятся сбоку. Левый столбец - общие отчеты. Правый - для докторских патрулей.
4. Подсчитанные данные отобразятся в следующем формате:

| ОХРАННАЯ СФЕРА:  | ПРОДОВОЛЬСТВЕННАЯ СФЕРА: | ВРАЧЕВАТЕЛЬНАЯ СФЕРА: | ВРАЧЕВАТЕЛЬНАЯ СФЕРА: |
| ------------- | ------------- | ------------- | ------------- |
| собирал ID n  | собирал ID n  | одиночный ID n  | собирал ID n  |
| вел ID n  | участвовал ID n | собирал ID n  | ID: |
| участвовал ID n  | помогал ID n  |  участвовал ID n  | поймано n мыши |
|  | ID:  |  | помогал ID n |
|   | yyy-mm-dd, поймал n  |   |  |

5. А дальше вы куда-нибудь копируете и начинаете обновлять таблицу, используя эти данные!

# Удачи вам, пираты!