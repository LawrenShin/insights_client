import {Pagination as PaginationType} from "../../components/lookupSearch/duck";

const paintRating = (value: string): string => `dot rating${value}`;

const prepareForGrid = (data: {companies: any[], pagination: PaginationType}) => {
  const {companies, pagination} = data;

  const columns = Object.keys(companies[0]).map((key: string) => {
    if (key === 'id') return {
      field: 'id',
      hide: true,
    }
    const lowerCasedKey = key.match(/[a-z]+|[A-Z][a-z]+/g)
      ?.join(' ')
      ?.toLowerCase();
    const headerName = lowerCasedKey?.replace(lowerCasedKey[0], lowerCasedKey[0].toUpperCase());
    const gridValid = {
      field: key,
      headerName,
      width: 200,
      // todo: provide later if needed
      // type:
    }
    if (key.match(/rating/gi)) return {
      ...gridValid,
      renderCell: ({value}: any) => {
        return <>
          <div className={paintRating(value)}></div>
          <span>{value}</span>
        </>
      }
    };
    return gridValid;
  });

  const rows = companies;

  return {columns, rows};
}

export default prepareForGrid;
