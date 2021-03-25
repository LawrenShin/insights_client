import {Pagination as PaginationType} from "../../components/lookupSearch/duck";

const paintRating = (value: string): string => `dot rating${value}`;

const WidthMatch: {[key: string]: number} = {
  'country': 150,
  'name': 250,
  'city': 200,
  'publicOrPrivate': 110,
  'status': 100,
  'essentialRating': 103,
  'essentialRatingDiversityScore': 103,
  'essentialRatingEquityAndInclusionScore': 103,
  'lei': 250,
  'id': 90,
}
export const setWidth = (key: string): number => WidthMatch[key] || 200;

const ratingRenderProvider = (gridValid: any) => ({
...gridValid,
    renderCell: ({value}: any) => {
    return <>
      <div className={paintRating(value)}></div>
      <span>{value}</span>
    </>
  }
});
const idRenderProvider = (gridValid: any, history: any) => ({
  ...gridValid,
  renderCell: ({value}: any) => {
    return <>
      <a
        href={'details'}
        onClick={(e) => {
          e.preventDefault();
          history.push(`/details/${value}`);
        }}
      >{value}</a>
    </>
  }
});

const prepareForGrid = (
  data: {
    companies: any[],
    pagination: PaginationType
  },
  history?: any
) => {

  const {companies, pagination} = data;

  const columns = Object.keys(companies[0]).map((key: string) => {

    // handle special behaviour
    if (key === 'id') return idRenderProvider({
      field: key,
      width: setWidth(key),
      headerName: 'DEI ID',
    }, history);
    if (key === 'lei') return {
      field: key,
      width: setWidth(key),
      headerName: key.toUpperCase(),
    }

    // sentence out of key
    const lowerCasedKey = key.match(/[a-z]+|[A-Z][a-z]+/g)
      ?.join(' ')
      ?.toLowerCase();
    const headerName = lowerCasedKey?.replace(lowerCasedKey[0], lowerCasedKey[0].toUpperCase());

    // ready object for grid cell
    const gridValid = {
      field: key,
      headerName,
      width: setWidth(key),
      // todo: provide later if needed
      // type:
    }

    //
    if (key.match(/rating/gi)) return ratingRenderProvider(gridValid);

    return gridValid;
  });

  const rows = companies;

  return {columns, rows};
}

export default prepareForGrid;
