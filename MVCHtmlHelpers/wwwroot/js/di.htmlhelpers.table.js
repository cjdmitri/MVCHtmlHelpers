//tfilterinput
const tfiltersinput = document.querySelectorAll('.tfilterinput');

if (tfiltersinput.length > 0) {
     for (let i = 0; i < tfiltersinput.length; i++) {
          let col = parseInt(tfiltersinput[i].dataset.col);
          tfiltersinput[i].addEventListener('input', function () {
               let value = tfiltersinput[i].value;
               tableFilter(value, 'tableBody', col);
          });
     }
}

function tableFilter(value, bodyTable, col = -1) {
     var rows = document.getElementById(bodyTable).rows;         //получаем все строки таблицы
     for (var i = 0; i < rows.length; i++) {                     //проход по всем строкам в таблице
          var inpvalue = value.toLowerCase();                 //Перефодим значение в нижний регистр
          if (inpvalue.length > 0) {                              //если количество символов > 0
               var cels = rows[i].getElementsByTagName('td');      //получаем все ячейки таблицы
               var matched = false;                                //переменная для определения совпадения
               var val;                                            //для получения значения ячейки
               if (col === -1) {                                   //если проход по всем колонкам таблицы
                    for (var td = 0; td < cels.length; td++) {      //то проходим по всем ячейкам таблицы
                         val = cels[td].innerHTML.toLowerCase();     //значение ячейки
                         if (val.indexOf(inpvalue) > -1) {           //сравниваем со значением инпута
                              matched = true;                         //совпадение
                              break;                                  //переходим к следующей строке
                         }
                    }
               } else {                                            //если поиск только в указанной колонке
                    val = cels[col].innerHTML.toLowerCase();        //значение ячейки указанной колонке в строке
                    if (val.indexOf(inpvalue) > -1) {               //если найдено совпадение
                         matched = true;//
                    }
               }
               if (matched === false) { //если после всех проверок не найдено совпадение
                    rows[i].hidden = true; //скрываем строку
               } else {
                    rows[i].hidden = false;
               }
          } else {                                                //если симолов для поиска нет, отображаем все строки
               rows[i].hidden = false;
          }
     }
     return;
}