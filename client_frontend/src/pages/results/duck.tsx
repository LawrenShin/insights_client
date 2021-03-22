import {call, put, takeLatest} from "typed-redux-saga";
import {getRequest} from "../../api";
import {CreateAction} from "../../store/actionType";
import {SignInType} from "../SignIn/duck";
import {RequestStatuses} from "../../api/requestTypes";

export enum ResultsActionType {
  RESULTS_LOAD = 'RESULTS_LOAD',
  RESULTS_SUCCESS = 'RESULTS_SUCCESS',
  RESULTS_FAIL = 'RESULTS_FAIL',
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
  data: null | any[];
  status: RequestStatuses;
  error: null | string;
}

const initState = {
  data: null,
  status: RequestStatuses.still,
  error: null,
}

// TODO: replace any
export function reducer (state: ResultsState = initState, action: any) {
  const {type, payload} = action;

  if (type === ResultsActionType.RESULTS_LOAD) return {
    ...state,
    status: RequestStatuses.loading,
  }
  if (type === ResultsActionType.RESULTS_SUCCESS) return {
    ...state,
    data: payload,
    status: RequestStatuses.still,
    error: null,
  }
  if (type === ResultsActionType.RESULTS_FAIL) return {
    ...state,
    status: RequestStatuses.fail,
    error: payload,
  }

  return state
}