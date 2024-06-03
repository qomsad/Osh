import axios from "axios";
import { notifications } from "@mantine/notifications";

let lastAbortId: string | null = null;
let lastSessionId: string | null = null;

export function ErrorHandler(error: unknown) {
  if (axios.isAxiosError(error)) {
    if (error.code && error.code == "ECONNABORTED") {
      if (lastAbortId == null) {
        lastAbortId = notifications.show({
          title: "Соединение прервано",
          color: "red",
          message: `Сервер не отвечает (10 секунд)`,
        });
      } else {
        notifications.update({
          id: lastAbortId,
          title: "Соединение прервано",
          color: "red",
          message: `Сервер не отвечает (10 секунд)`,
        });
      }
    }
    if (error.code && error.code == "ERR_BAD_REQUEST" && error.response && error.response.status === 401) {
      if (lastSessionId == null) {
        lastSessionId = notifications.show({
          title: "Сессия прервана",
          color: "red",
          message: `Требуется перезайти в аккаунт`,
        });
      } else {
        notifications.update({
          id: lastSessionId,
          title: "Сессия прервана",
          color: "red",
          message: `Требуется перезайти в аккаунт`,
        });
      }
    }
    if (error.response && error.response.data && error.response.data.description) {
      notifications.show({
        title: "Ошибка",
        color: "red",
        message: `${error.response.data.description}`,
      });
    }
    if (error.response && error.response.data && Array.isArray(error.response.data)) {
      for (const errorKey of error.response.data) {
        if (errorKey.description)
          notifications.show({
            title: "Проверьте заполнение полей",
            color: "red",
            message: `${errorKey.description}`,
          });
      }
    }
  } else {
    console.log(error);
    notifications.show({
      title: "Неизвестная ошибка",
      color: "red",
      message: "",
    });
  }
}
