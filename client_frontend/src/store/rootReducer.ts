import {combineReducers} from "redux";
import {reducer as SignIn} from '../pages/SignIn/duck';
import {reducer as Register} from '../components/forms/register/duck';
import {reducer as LookupSearch} from "../components/lookupSearch/duck";

const rootReducer = combineReducers({
  SignIn,
  Register,
  LookupSearch,
});

export type RootState = ReturnType<typeof rootReducer>

export default rootReducer;
