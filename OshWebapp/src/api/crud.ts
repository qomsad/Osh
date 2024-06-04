import { SearchPageRes } from "../models/SearchPageRes.ts";
import { ErrorHandler } from "./ErrorHadler.ts";
import { AxiosInstance } from "axios";

export interface Api<T> {
  search(searchValue: string, pageIndex: number, pageSize: number): Promise<SearchPageRes<T>>;

  getById(id: number): Promise<T | null>;

  create(entity: Partial<T>): Promise<string>;

  update(id: number, entity: Partial<T>): Promise<string>;

  delete(id: number): Promise<string>;
}

export function getQuery<T>(url: string, inst: () => AxiosInstance): Api<T> {
  return {
    async search(searchValue: string, pageIndex: number, pageSize: number): Promise<SearchPageRes<T>> {
      return await inst()
        .get<SearchPageRes<T>>(url, {
          params: {
            SearchString: searchValue == "" ? undefined : searchValue,
            PageIndex: pageIndex + 1,
            PageSize: pageSize,
          },
        })
        .then(({ data }) => data)
        .catch((err) => {
          ErrorHandler(err);
          return {
            content: [],
            totalCount: 0,
          };
        });
    },
    async getById(id: number): Promise<T | null> {
      return await inst()
        .get<T>(`${url}/${id}`)
        .then(({ data }) => data)
        .catch((err) => {
          ErrorHandler(err);
          return null;
        });
    },
    async create(entity: Partial<T>): Promise<string> {
      return await inst()
        .post<T>(`${url}`, { ...entity })
        .then(({ data, statusText }) => {
          if (Object.prototype.hasOwnProperty.call(data, "id")) {
            let vars = data as any;
            vars = vars as { id: number };
            return vars.id.toString();
          }
          return statusText;
        })
        .catch((err) => {
          ErrorHandler(err);
          return "ERROR";
        });
    },
    async update(id: number, entity: Partial<T>): Promise<string> {
      return await inst()
        .put<T>(`${url}/${id}`, { ...entity })
        .then(({ statusText }) => statusText)
        .catch((err) => {
          ErrorHandler(err);
          return "ERROR";
        });
    },
    async delete(id: number): Promise<string> {
      return await inst()
        .delete<T>(`${url}/${id}`)
        .then(({ statusText }) => statusText)
        .catch((err) => {
          ErrorHandler(err);
          return "ERROR";
        });
    },
  };
}
