import {RequestStatuses} from '../../../api/requestTypes';
import {takeLatest, call, put} from "redux-saga/effects";
import {postRequest} from "../../../api";
import {CreateAction} from "../../../store/actionType";
// TODO: types

export enum RegisterTypes {
  REGISTER = 'REGISTER',
  REGISTER_SUCCESS = 'REGISTER_SUCCESS',
  REGISTER_FAIL = 'REGISTER_FAIL',
}

// SAGAS
export function* worker (action: any) {
  try {
    const res = yield call(postRequest, 'register', action.payload);
    yield put(CreateAction(RegisterTypes.REGISTER_SUCCESS, res));
  } catch(e: any) {
    yield put(CreateAction(RegisterTypes.REGISTER_FAIL, e));
  }
}

export function* watcher () {
  yield takeLatest(RegisterTypes.REGISTER, worker);
}

// REDUCER

const initState = {
  status: RequestStatuses.still,
  error: null,
}

export function reducer (state: {
  status: RequestStatuses;
  error: null | string;
} = initState, action: any) {
  const {type, payload} = action;

  if (type === RegisterTypes.REGISTER) return {
    ...state,
    status: RequestStatuses.loading,
  }
  if (type === RegisterTypes.REGISTER_SUCCESS) return {
    ...state,
    data: payload,
    status: RequestStatuses.still,
    error: null,
  }
  if (type === RegisterTypes.REGISTER_FAIL) return {
    ...state,
    status: RequestStatuses.fail,
    error: payload,
  }

  return state
}