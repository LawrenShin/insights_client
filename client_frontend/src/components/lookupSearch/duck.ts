import {RequestStatuses} from "../../api/requestTypes";
import {takeLatest} from "redux-saga/effects";
import {call, put} from "typed-redux-saga";
import {getRequest} from "../../api";
import {CreateAction} from "../../store/actionType";

export default () => null

export enum LookupSearchActionType {
  LOOKUP_LOAD = 'LOOKUP_LOAD',
  LOOKUP_LOAD_SUCCESS = 'LOOKUP_LOAD_SUCCESS',
  LOOKUP_LOAD_FAIL = 'LOOKUP_LOAD_FAIL',
  LOOKUP_LOAD_CLEAR = 'LOOKUP_LOAD_CLEAR',
}

export interface Action {
  type: LookupSearchActionType;
  payload?: any;
}

// SAGAS
export function* worker(action: any) {
  const {url, params} = action.payload;
  try {
    const res = yield call(getRequest, url, params);
    yield put(CreateAction(LookupSearchActionType.LOOKUP_LOAD_SUCCESS, res));
  } catch(error) {
    yield put(CreateAction(LookupSearchActionType.LOOKUP_LOAD_FAIL, error));
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
  pageIndex: number,
  pageCount: number,
}

export interface State {
  data: {
    companies: Company[] | [],
    pagination: Pagination,
  }
  status: RequestStatuses;
  error: string | null;
}

export const initialState = {
  data: {
    companies: [],
    pagination: {
      pageSize: 4,
      pageIndex: 1,
      pageCount: 0,
    },
  },
  status: RequestStatuses.still,
  error: null,
}

export function reducer(state: State = initialState, action: Action) {
  const {type, payload} = action;
  if (type === LookupSearchActionType.LOOKUP_LOAD) return {...state, status: RequestStatuses.loading};

  if (type === LookupSearchActionType.LOOKUP_LOAD_SUCCESS) return {
    ...state,
    data: {
      ...state.data,
      companies: payload.companies,
      pagination: payload.pagination,
    },
    status: RequestStatuses.still,
  }

  if (type === LookupSearchActionType.LOOKUP_LOAD_CLEAR) return initialState;

  return state;
}