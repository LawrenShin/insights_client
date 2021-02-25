import {RequestStatuses} from '../../api/requestTypes';
import {takeLatest, call, put} from "redux-saga/effects";
import {postRequest} from "../../api";
import {CreateAction} from "../../store/actionType";
// TODO: types

export enum SignInType {
  SIGN_IN = 'SIGN_IN',
  SIGN_IN_SUCCESS = 'SIGN_IN_SUCCESS',
  SIGN_IN_FAIL = 'SIGN_IN_FAIL',
}

// SAGAS
export function* worker (action: any) {
  try {
    const res = yield call(postRequest, 'login', action.payload);
    yield put(CreateAction(SignInType.SIGN_IN_SUCCESS, res));
  } catch(e: any) {
    yield put(CreateAction(SignInType.SIGN_IN_FAIL, e));
  }
}

export function* watcher () {
  yield takeLatest(SignInType.SIGN_IN, worker);
}

// REDUCER

const initState = {
  data: null,
  status: RequestStatuses.still,
  error: null,
}

export function reducer (state: any = initState, action: any) {
  const {type, payload} = action;

  if (type === SignInType.SIGN_IN) return {
    ...state,
    status: RequestStatuses.loading,
  }
  if (type === SignInType.SIGN_IN_SUCCESS) return {
    ...state,
    data: payload,
    status: RequestStatuses.still,
    error: null,
  }
  if (type === SignInType.SIGN_IN_FAIL) return {
    ...state,
    status: RequestStatuses.fail,
    error: payload,
  }

  return state
}