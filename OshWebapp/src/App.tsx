import { useEffect, useState } from "react";

function App() {
  const [data, setData] = useState<string>();

  useEffect(() => {
    (async function () {
      const response = await fetch("/api/test");
      const t = await response.text();
      setData(t);
    })();
  }, []);

  const content = data === undefined ? <p>Loading</p> : <p>{data}</p>;

  return <>{content}</>;
}

export { App };
