import {HttpErrorResponse} from "@angular/common/http";

export class ErrorMessages {

  /**
   * Converts HttpErrorResponse to text representation
   * @param error
   */
  public static getTextMessage(error: HttpErrorResponse): string {
    switch (error.status) {
      case 401:
        return "Вам необходимо авторизоваться"
      case 403:
        return "У вас нет полномочий для этого"
      case 404:
        return "Страница не найдена"
      case 409:
        return "Пользователь с таким логином уже существует"
      case 412:
        return "Некорректные данные"
      case 423:
        return "Логин занят. Используйте другой"
      default:
        return "Внутренняя ошибка. Сообщите администрации"
    }
  }
}

