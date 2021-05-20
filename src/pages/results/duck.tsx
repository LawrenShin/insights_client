import {call, put, select, takeLatest} from "typed-redux-saga";
import {getRequest, postRequest} from "../../api";
import {CreateAction} from "../../store/actionType";
import {SignInType} from "../SignIn/duck";
import {RequestStatuses} from "../../api/requestTypes";
import {Pagination as PaginationType} from "../../components/lookupSearch/duck";
import {AnyAction} from "redux";
import {RootState} from "../../store/rootReducer";

export enum ResultsActionType {
  RESULTS_LOAD = 'RESULTS_LOAD',
  RESULTS_SUCCESS = 'RESULTS_SUCCESS',
  RESULTS_FAIL = 'RESULTS_FAIL',
  RESULTS_PAGINATION = 'RESULTS_PAGINATION',
  RESULTS_CLEAR = 'RESULTS_CLEAR',
  RESULTS_SET_TAB = 'RESULTS_SET_TAB',
}
// SELECTORS
const getOptions = (state: RootState) => state.LookupSearch.data.options;
const getPagination = (state: RootState) => state.LookupSearch.data.pagination;

// SAGAS
export function* worker (action: any) {
  const {url, params} = action.payload;
  const options = yield select(getOptions);

  try {
    const res = yield url === 'companies' ?
      call(getRequest, url, params)
      :
      call(postRequest, url, {ids: options});

    yield put(CreateAction(ResultsActionType.RESULTS_SUCCESS, res));
  } catch(error: any) {
    if (error.message === '403') return yield put(CreateAction(SignInType.LOGOUT));
    yield put(CreateAction(ResultsActionType.RESULTS_FAIL, error.message));
  }
}

export function* watcher () {
  yield takeLatest(ResultsActionType.RESULTS_LOAD, worker);
}

// TYPE GUARDS
export const isCompanies = (items: (CompanyLookup | Industry)[] | undefined | null): items is CompanyLookup[] => {
  if (items) {
    return (items[0] as CompanyLookup)?.lei !== undefined;
  }
  return false;
}
export const isIndustries = (item: (CompanyLookup | Industry)[]): item is Industry[] => {
  return ((item[0] as Industry)?.type !== undefined) || ((item[0] as Industry)?.averageAdvancedDEIFocus !== undefined);
}

// REDUCER
// this relates to company details
type GenCompanyInfo = {
  address: string,
  id: string,
  industries: [],
  lei?: string,
}
export interface Industry {
  id: number,
  name: string,
  type: string,
  averageAdvancedDEIFocus?: number,
  averageAdvancedForecast?: number,
  averageAdvancedGenderBoard?: number,
  averageAdvancedGenderExecutive?: number,
  averageAdvancedInclusion?: number,
  averageAdvancedRaceBoard?: number,
  averageAdvancedRaceExecutive?: number,
  averageAdvancedTotal?: number,
  averageEssentialDiversity?: number,
  averageEssentialEquityAndInclusion?: number,
  averageEssentialTotal?: number,
}
export interface CompanyLookup {
  id: number,
  name: string,
  country?: string,
  city?: string,
  postcode?: string,
  street1?: string,
  publicOrPrivate?: string,
  status?: string,
  essential?: string,
  essentialDiversityScore?: string,
  essentialEquityAndInclusionScore?: string,
  lei?: string,
}
export interface ResultsState {
  data: {
    // TODO: replace any with industries and companies types
    items: (CompanyLookup | Industry)[] | null,
    pagination: PaginationType
  };
  tab: string,
  status: RequestStatuses;
  error: null | string;
}

const initState = {
  data: {
    items: null,
    pagination: {
      pageNumber: 1,
      pageSize: 10,
      pageCount: 0,
    }
  },
  tab: localStorage.getItem('tab') || 'company',
  status: RequestStatuses.still,
  error: null,
}

// TODO: replace any
export function reducer (state: ResultsState = initState, action: AnyAction) {
  const {type, payload} = action;

  if (type === ResultsActionType.RESULTS_LOAD) return {
    ...state,
    status: RequestStatuses.loading,
  }
  if (type === ResultsActionType.RESULTS_SUCCESS) {
    const {companies, industries} = payload;
    return {
      ...state,
      data: {
        items: companies || industries,
        pagination: state.data.pagination.pageCount !== payload.pagination.pageCount ?
          payload.pagination : state.data.pagination,
      },
      status: RequestStatuses.still,
      error: null,
    }
  }
  if (type === ResultsActionType.RESULTS_FAIL) return {
    ...state,
    status: RequestStatuses.fail,
    error: payload,
  }
  if (type === ResultsActionType.RESULTS_PAGINATION) return {
    ...state,
    data: {
      ...state.data,
      pagination: {
        ...payload
      }
    }
  }
  if (type === ResultsActionType.RESULTS_SET_TAB) {
    localStorage.setItem('tab', payload);
    return {
      ...state,
      tab: payload,
    }
  }
  if (type === ResultsActionType.RESULTS_CLEAR) {
    return {
      ...initState,
      tab: localStorage.getItem('tab') || 'company',
    };
  }

  return state
}