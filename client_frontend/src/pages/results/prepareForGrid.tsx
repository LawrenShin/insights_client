import {Pagination as PaginationType} from "../../components/lookupSearch/duck";
import {keyTitle} from "../../helpers";

const paintRating = (value: string): string => `dot rating${value}`;

const WidthMatch: {[key: string]: number} = {
  'country': 150,
  'name': 250,
  'city': 200,
  'publicOrPrivate': 110,
  'status': 100,
  'essentialRating': 170,
  'essentialRatingDiversityScore': 170,
  'essentialRatingEquityAndInclusionScore': 170,
  'lei': 230,
  'id': 100,
  'postcode': 115,
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
const nameRenderProvider = (gridValid: any, history: any) => ({
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



const prepareForGrid = <S extends {[key: string]: string}>(
  data: {
    companies: any[],
    pagination: PaginationType
  },
  styles: S,
  history?: any,
) => {

  const {companies} = data;

  const columns = Object.keys(companies[0]).map((key: string) => {

    // sentence out of key
    const headerName = key === 'id' ? 'DEI ID' :
      key === 'lei' ? key.toUpperCase() : keyTitle(key);

    // ready object for grid cell
    const gridValid = {
      field: key,
      headerName,
      width: setWidth(key),
    };

    if (key.match(/rating/gi)) return ratingRenderProvider(gridValid);
    if (key === 'name') return nameRenderProvider(gridValid, history);

    return gridValid;
  });

  const rows = companies;

  return {columns, rows};
}

export default prepareForGrid;
