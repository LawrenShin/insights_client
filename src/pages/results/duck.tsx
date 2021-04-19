import {call, put, takeLatest} from "typed-redux-saga";
import {getRequest} from "../../api";
import {CreateAction} from "../../store/actionType";
import {SignInType} from "../SignIn/duck";
import {RequestStatuses} from "../../api/requestTypes";
import {Pagination as PaginationType} from "../../components/lookupSearch/duck";
import {AnyAction} from "redux";

export enum ResultsActionType {
  RESULTS_LOAD = 'RESULTS_LOAD',
  RESULTS_SUCCESS = 'RESULTS_SUCCESS',
  RESULTS_FAIL = 'RESULTS_FAIL',
  RESULTS_PAGINATION = 'RESULTS_PAGINATION',
  RESULTS_CLEAR = 'RESULTS_CLEAR',
}

// SAGAS
export function* worker (action: any) {
  const {url, params} = action.payload;
  try {
    const res = yield call(getRequest, url, params);
    yield put(CreateAction(ResultsActionType.RESULTS_SUCCESS, res));
  } catch(error: any) {
    if (error.message === '403') return yield put(CreateAction(SignInType.LOGOUT));
    yield put(CreateAction(ResultsActionType.RESULTS_FAIL, error.message));
  }
}

export function* watcher () {
  yield takeLatest(ResultsActionType.RESULTS_LOAD, worker);
}

// REDUCER
export interface ResultsState {
  // TODO: replace any
  data: {
    companies: any[] | null,
    pagination: PaginationType
  };
  status: RequestStatuses;
  error: null | string;
}

const initState = {
  data: {
    companies: null,
    pagination: {
      pageNumber: 1,
      pageSize: 10,
      pageCount: 0,
    }
  },
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
  if (type === ResultsActionType.RESULTS_SUCCESS) return {
    ...state,
    data: {
      companies: payload.companies,
      pagination: state.data.pagination.pageCount !== payload.pagination.pageCount ?
        payload.pagination : state.data.pagination,
    },
    status: RequestStatuses.still,
    error: null,
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
  if (type === ResultsActionType.RESULTS_CLEAR) return initState;

  return state
}