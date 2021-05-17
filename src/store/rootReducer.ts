import {combineReducers} from "redux";
import {reducer as SignIn} from '../pages/SignIn/duck';
import {reducer as Results} from '../pages/results/duck';
import {reducer as Details} from '../pages/details/duck';
import {reducer as Register} from '../components/forms/register/duck';
import {reducer as LookupSearch} from "../components/lookupSearch/duck";
import createReducer from "./createReducer";

const rootReducer = combineReducers({
  SignIn,
  Register,
  LookupSearch,
  Results,
  Details,
  Dictionaries: createReducer('DICTIONARIES'),
});

export type RootState = ReturnType<typeof rootReducer>

export default rootReducer;
