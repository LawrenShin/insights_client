import {RequestStatuses} from '../../api/requestTypes';
import {takeLatest, put} from "redux-saga/effects";
import {call} from "typed-redux-saga";
import {postRequest} from "../../api";
import {CreateAction} from "../../store/actionType";


export enum SignInType {
  SIGN_IN = 'SIGN_IN',
  SIGN_IN_SUCCESS = 'SIGN_IN_SUCCESS',
  SIGN_IN_FAIL = 'SIGN_IN_FAIL',
  LOGOUT = 'LOGOUT',
}

// SAGAS
export function* worker (action: any) {
  try {
    const res = yield call(postRequest, 'login', action.payload);
    yield put(CreateAction(SignInType.SIGN_IN_SUCCESS, res));
  } catch(error: any) {
    if (error.message === '403') return yield put(CreateAction(SignInType.LOGOUT));
    yield put(CreateAction(SignInType.SIGN_IN_FAIL, error.message));
  }
}

export function* watcher () {
  yield takeLatest(SignInType.SIGN_IN, worker);
}

// REDUCER
interface UserData {
  token: string;
  accessRights: string[];
}
interface InitialState {
  data: UserData | null;
  status: RequestStatuses;
  error: string | null;
}
const fromStorage = localStorage.getItem('user');
const initState = {
  data: fromStorage ? JSON.parse(fromStorage) : null,
  status: RequestStatuses.still,
  error: null,
}

export function reducer (state: InitialState = initState, action: any) {
  const {type, payload} = action;

  if (type === SignInType.SIGN_IN) return {
    ...state,
    status: RequestStatuses.loading,
  }
  if (type === SignInType.SIGN_IN_SUCCESS) {
    localStorage.setItem('user', JSON.stringify(payload))
    return {
      ...state,
      data: payload,
      status: RequestStatuses.still,
      error: null,
    }
  }

  if (type === SignInType.SIGN_IN_FAIL) return {
    ...state,
    status: RequestStatuses.fail,
    error: payload,
  }

  if (type === SignInType.LOGOUT) {
    localStorage.clear();
    return {
      ...state,
      data: null,
    }
  }

  return state
}