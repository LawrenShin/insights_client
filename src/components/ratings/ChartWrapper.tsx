import React, {FunctionComponent} from 'react';

type Props = { height?: string, width?: string };

const ChartWrapper: FunctionComponent<Props> = ({height, width, children}) => {
  return (
    <div style={{position: 'relative', height: height || '100px', width: width || '100%'}}>
      <div style={{position: 'absolute', height: height || '100px', width: width || '100%'}}>
        {children}
      </div>
    </div>
  );
}

export default ChartWrapper;
