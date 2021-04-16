import {RequestStatuses} from "../../api/requestTypes";
import {AnyAction} from "redux";
import {takeLatest, call, put} from "typed-redux-saga";
import {getRequest} from "../../api";
import {CreateAction} from "../../store/actionType";
import {SignInType} from "../SignIn/duck";

// TODO: all similar ducks could be refactored. States, actions, requests, sagas, reducers r almost the same

export enum DetailsActionType {
  DETAILS_LOAD = 'DETAILS_LOAD',
  DETAILS_SUCCESS = 'DETAILS_SUCCESS',
  DETAILS_FAIL = 'DETAILS_FAIL',
}


export function* worker(action: AnyAction) {
  const {url, params} = action.payload;
  try {
    const res = yield call(getRequest, url, params);
    yield put(CreateAction(DetailsActionType.DETAILS_SUCCESS, res));
  } catch(error: any) {
    if (error.message === '403') return yield put(CreateAction(SignInType.LOGOUT));
    yield put(CreateAction(DetailsActionType.DETAILS_FAIL, error.message));
  }
}
export function* watcher() {
  yield takeLatest(DetailsActionType.DETAILS_LOAD, worker);
}

interface State {
  data: any;
  status: RequestStatuses;
  error: string | null;
}

const initState = {
  data: null,
  status: RequestStatuses.still,
  error: null,
}

export function reducer (state: State = initState, action: AnyAction) {
  const {type, payload} = action;

  if (type === DetailsActionType.DETAILS_FAIL) return {
    ...state,
    error: payload,
  }

  if (type === DetailsActionType.DETAILS_LOAD) return {
    ...state,
    status: RequestStatuses.loading,
  }

  if (type === DetailsActionType.DETAILS_SUCCESS) return {
    ...state,
    data: payload,
    status: RequestStatuses.still,
  }

  return state;
}