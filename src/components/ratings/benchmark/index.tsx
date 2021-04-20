import React from 'react';
import TriangleSvg from "../../TriangleSvg";
import {useStyles} from "./useStyles";
import _ from "lodash";


const Benchmark = (props: any) => {
  const {
    containerCol,
    containerRow,
    indicator,
    progressBar,
    barParts,
    ratingNames,
    part, partA, partB, partC,
    paleFont,
    triangle,
    dialog,
    dialogTriangle,
    gap5,
  } = useStyles();

  const renderIndicator = (isCompany?: boolean) => <div className={`${indicator}`}>
    <div className={isCompany ? dialog : ''}>
      {isCompany && <span className={paleFont}>COMPANY</span>}
      <div className={isCompany ? dialogTriangle : ''}> </div>
    </div>
    <TriangleSvg pale={!isCompany} styles={triangle} />
  </div>;

  const renderRatingBar = (name: string) => <div
    className={`
      ${part} 
      ${name === 'C' ? 
        partC : name === 'B' ? 
          partB : partA}
    `}
  >
    {name === 'C' && <span>D</span>}
    {[1, 2, 3].map((n) => <span>{_.times(n, () => name)}</span>)}
  </div>

  return (<div className={`${containerCol} ${gap5}`}>
    <div className={containerRow}>
      {renderIndicator()}
      {renderIndicator(true)}
    </div>

    <div className={progressBar}>
      <div className={`${barParts} ${paleFont}`}>
        {renderRatingBar('C')}
        {renderRatingBar('B')}
        {renderRatingBar('A')}
      </div>
    </div>
  </div>);
}

export default Benchmark;