import {RequestStatuses} from "../../api/requestTypes";
import {takeLatest} from "redux-saga/effects";
import {call, put} from "typed-redux-saga";
import {getRequest} from "../../api";
import {CreateAction} from "../../store/actionType";
import {SignInType} from "../../pages/SignIn/duck";

export enum PaginationActionTypes {
  INCREMENT = 'INCREMENT',
  DEREMENT = 'DECREMENT',
}

export enum LookupSearchActionType {
  LOOKUP_LOAD = 'LOOKUP_LOAD',
  SAVE_SEARCH = 'SAVE_SEARCH',
  SAVE_INDUSTRY_OPTIONS = 'SAVE_INDUSTRY_OPTIONS',
  LOOKUP_LOAD_SUCCESS = 'LOOKUP_LOAD_SUCCESS',
  LOOKUP_LOAD_FAIL = 'LOOKUP_LOAD_FAIL',
  LOOKUP_LOAD_CLEAR = 'LOOKUP_LOAD_CLEAR',
}

export interface Action {
  type: LookupSearchActionType | PaginationActionTypes;
  payload?: any;
}

// SAGAS
export function* worker(action: any) {
  const {url, params} = action.payload;
  try {
    const res = yield call(getRequest, url, params);
    yield put(CreateAction(LookupSearchActionType.LOOKUP_LOAD_SUCCESS, res));
  } catch(error) {
    if (error.message === '403') return yield put(CreateAction(SignInType.LOGOUT));
    yield put(CreateAction(LookupSearchActionType.LOOKUP_LOAD_FAIL, error.message));
  }
}

export function* watcher() {
  yield takeLatest(LookupSearchActionType.LOOKUP_LOAD, worker);

}

// REDUCER
export interface Company {
    id: number,
    name: string,
    country: string,
    city: string,
  }

export interface Pagination {
  pageSize: number,
  pageNumber: number,
  pageCount: number,
}

export interface State {
  data: {
    companies: Company[] | [],
    pagination: Pagination,
    search: string,
    // NOTE: for now like that. If added more kinds of options maybe thats the place
    options: number[],
  }
  status: RequestStatuses;
  error: string | null;
}

export const initialState = {
  data: {
    search: '',
    companies: [],
    // NOTE: options might relate to both companies and industries thus left naming abstract
    options: [],
    pagination: {
      pageSize: 10,
      pageNumber: 1,
      pageCount: 0,
    },
  },
  status: RequestStatuses.still,
  error: null,
}

export function reducer(state: State = initialState, action: Action) {
  const {type, payload} = action;
  if (type === LookupSearchActionType.LOOKUP_LOAD) return {...state, status: RequestStatuses.loading};

  // NOTE: not sure what this is for
  if (type === LookupSearchActionType.SAVE_SEARCH) return {
    ...state,
    data: {
      ...state.data,
      search: payload,
    },
  };

  if (type === LookupSearchActionType.SAVE_INDUSTRY_OPTIONS) {
    const {id, checked} = payload;
    const {options} = state.data;
    return {
      ...state,
      data: {
        ...state.data,
        options: checked ?
          [...options, id] : options.filter(opt => opt !== id),
      },
    }
  }

  if (type === LookupSearchActionType.LOOKUP_LOAD_SUCCESS) return {
    ...state,
    data: {
      ...state.data,
      companies: [...state.data.companies, ...payload.companies],
      pagination: payload.pagination,
    },
    status: RequestStatuses.still,
  }

  if (type === LookupSearchActionType.LOOKUP_LOAD_CLEAR) return initialState;

  if (type === PaginationActionTypes.INCREMENT) {
    const {pageNumber} = state.data.pagination;
    return {
      ...state,
      data: {
        ...state.data,
        pagination: {
          ...state.data.pagination,
          pageNumber: pageNumber + 1,
        }
      }
    }
  }

  return state;
}