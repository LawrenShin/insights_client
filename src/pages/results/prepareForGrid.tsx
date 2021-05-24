import {Pagination as PaginationType} from "../../components/lookupSearch/duck";
import {keyTitle} from "../../helpers";
import {CompanyLookup, Industry, isIndustries} from "./duck";

export const paintRatingClass = (value: string): string => `dot rating${value}`;

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
      <div className={paintRatingClass(value)}> </div>
      <span>{value}</span>
    </>
  }
});

const nameRenderProvider = (gridValid: any, history: any) => ({
  ...gridValid,
  renderCell: ({value, row: {id}}: any) => {
    return <>
      <a
        href={'details'}
        onClick={(e) => {
          e.preventDefault();
          history.push(`/details/${id}`);
        }}
      >{value}</a>
    </>
  }
});


// TODO: create another for idustries
const prepareForGrid = <S extends {[key: string]: string}>(
  data: {
    items: (CompanyLookup | Industry)[],
    pagination: PaginationType
  },
  styles: S,
  history?: any,
) => {

  const {items} = data;

  const columns = Object.keys(items[0]).map((key: string) => {
    const isId = key.match(/id/gi);

    // sentence out of key
    const headerName = isId ? 'DEI ID' :
      key === 'lei' ? key.toUpperCase() : keyTitle(key);

    // ready object for grid cell
    const gridValid = {
      field: key,
      headerName,
      width: setWidth(key),
    };

    if (isIndustries(items)) {
      if (isId) return {...gridValid, hide: true};
      if (key !== 'name' && key !== 'type' && !isId) {
        return ratingRenderProvider(gridValid);
      }
      if (key === 'name') return nameRenderProvider(gridValid, history);
    } else {
      if (key.match(/essential/gi)) return ratingRenderProvider(gridValid);
      if (key === 'name') return nameRenderProvider(gridValid, history);
    }


    return gridValid;
  });

  const rows = items;

  return {columns, rows};
}

export default prepareForGrid;
