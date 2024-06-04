export function passGen() {
  // Функция для генерации случайного числа от 0 до 999
  function getRandomThreeDigitNumber() {
    return Math.floor(Math.random() * 1000)
      .toString()
      .padStart(3, "0");
  }

  // Генерируем три части по три цифры каждая
  const part1 = getRandomThreeDigitNumber();
  const part2 = getRandomThreeDigitNumber();
  const part3 = getRandomThreeDigitNumber();

  // Объединяем части в нужный формат
  return `${part1}.${part2}.${part3}`;
}
