export const locale = {
  globalSearch: "Поиск",
  pageSize: <span style={{ marginRight: "10pt" }}>Количество элементов на странице:</span>,
  pagination: (firstRowNum: any, lastRowNum: any, maxRows: any) => (
    <>
      Представлены <b>{firstRowNum}</b> - <b>{lastRowNum}</b> из <b>{maxRows}</b>
    </>
  ),
};
