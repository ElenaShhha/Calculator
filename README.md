ENG

A functional calculator supporting basic arithmetic operations with integers has been developed. The calculator interface includes a display and a keypad.

Main functionality:
- The display shows the currently entered number or the result of the last operation
- The keypad contains buttons for digits 0-9, arithmetic operations '+', '-', '/', '=', 'C' button to clear the last entry, and 'AC' button to clear all

Key features:
- Number entry supports sequences up to 8 digits long. Any digits entered beyond 8 are ignored
- Pressing an operation button displays the result between:
  * the result of the previous operation and the last number entered
  * the last two numbers entered
  * the last number entered alone
- The 'C' button clears the last number or operation entered. If the last entry was an operation, the display will revert to the preceding value
- The 'AC' button clears all internal work areas and sets the display to 0
- If any operation exceeds the 8-digit maximum - ERR

Additional capabilities:
- A '+/-' button allows changing the sign of the currently displayed number
- A decimal point ('.') button allows entering floating-point numbers up to 3 decimal places and performing operations with the maximum number of decimal places entered for any one number

During development, the main functional features of standard mobile device calculators were taken into account, including handling various edge cases and errors. The eval() function was not used for calculations.


RUS
Был разработан функциональный калькулятор, поддерживающий основные арифметические операции с целыми числами. Интерфейс калькулятора включает дисплей и панель управления.

Основной функционал:
- Дисплей отображает текущее введенное число или результат последней операции
- Панель управления содержит кнопки для цифр 0-9, арифметических операций '+', '-', '/', '=', кнопку 'C' для удаления последнего ввода и кнопку 'AC' для полной очистки

Особенности работы:
- Поддерживается ввод чисел до 8 цифр в длину. Ввод более длинных чисел игнорируется
- При нажатии на кнопку операции отображается результат:
  * между результатом предыдущей операции и последним введенным числом
  * между двумя последними введенными числами
  * с единственным введенным числом
- Кнопка 'C' удаляет последнее введенное число или операцию
- Кнопка 'AC' полностью очищает все внутренние данные и устанавливает на дисплее значение 0
- При попытке вычислить результат, превышающий 8 цифр, на дисплее - ERR

Дополнительные возможности:
- Кнопка '+/-' позволяет изменять знак текущего отображаемого числа
- Добавлена поддержка десятичной точки '.', позволяющая работать с числами с плавающей точкой до 3 знаков после запятой

В процессе разработки были учтены основные функциональные особенности стандартных калькуляторов мобильных устройств, включая обработку различных граничных случаев и ошибок. Для выполнения вычислений не использовалась функция eval().
